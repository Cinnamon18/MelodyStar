using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace FluidSynth {
    public class Middleware : MiddlewareAPI {
        const String pkg = "FluidSynth";
        
        // FluidSynth objects
        IntPtr
            settings    = IntPtr.Zero,
            synth       = IntPtr.Zero,
            audioDriver = IntPtr.Zero,
            midiDriver  = IntPtr.Zero;
        int sfont = 0;

        // MIDI event callback accessible variable wrapper
        GCHandle midiCallbackEnv;
        
        // MIDI event user callbacks & environments (variable wrappers)
        OnMIDI
            onPress = (MIDINote midi, object env) => { return midi; },
            onRelease = (MIDINote midi, object env) => { return midi; };
        object
            onPressEnv = null,
            onReleaseEnv = null;
        
        /*
         * Setup and initialize library.
         * @param sample rate for output audio
         * @param period buffers for output audio
         * @param period buffer size for output audio
         * @param whether to autoconnect to an available MIDI device
         * @param Unity AudioDevice to output to instead of system audio
         */
        public Middleware(float audioGain       = 0.2f,
                          int   midiChannels    = 256,
                          int   audioSampleRate = 44100,
                          int   audioPeriods    = 8,
                          int   audioPeriodSize = 512,
                          bool  hotplugMIDI     = true,
                          AudioSource unityAudioProvider = null) {
            {   // Create settings
                settings = Wrapper.new_fluid_settings();
                Util.Assert(settings != IntPtr.Zero,
                            "{0}: Failed to create settings.", pkg);
                Wrapper.fluid_settings_setnum
                    (settings, "synth.gain", audioGain);
                Wrapper.fluid_settings_setint
                    (settings, "synth.sample-rate", audioSampleRate);
                Wrapper.fluid_settings_setint
                    (settings, "audio.periods", audioPeriods);
                Wrapper.fluid_settings_setint
                    (settings, "audio.period-size", audioPeriodSize);
                Wrapper.fluid_settings_setint
                    (settings, "midi.autoconnect", 0);
            }
            
            {   // Create MIDI synthesizer
                synth = Wrapper.new_fluid_synth(settings);
                Util.Assert(synth != IntPtr.Zero,
                            "{0}: Failed to create synth.", pkg);
            }
            
            {   // Setup audio output
                // Link synth to Unity AudioSource if provided
                if (unityAudioProvider == null) {
                    // Create audio driver
                    audioDriver
                        = Wrapper.new_fluid_audio_driver(settings, synth);
                    Util.Assert(audioDriver != IntPtr.Zero,
                                "{0}: Failed to create driver.", pkg);
                } else {
                    unityAudioProvider.clip = AudioClip.Create
                        ("FluidSynth", 2, 2, audioSampleRate,
                         true, (float[] buf) => {
                            //if(synth != IntPtr.Zero)
                            Wrapper.fluid_synth_write_float
                            (synth, buf.Length, buf, 0, 2, buf, 1, 2);
                        });
                    unityAudioProvider.loop = true;
                    unityAudioProvider.Play();
                }
            }
            
            // MIDI device input driver
            if (hotplugMIDI) {
                if (((MiddlewareAPI) this).ConnectMIDIDevice())
                    Debug.Log(String.Format("{0}: MIDI device connected!",
                                            pkg));
                else
                    Debug.Log(String.Format("{0}: Could not find MIDI device!",
                                            pkg));
              
                /*
                var onDeviceConnect = () => {
                    if (!MiddlewareAPI.IsMIDIDeviceConnected())
                        if(MiddlewareAPI.ConnectMIDIDevice())
                            Debug.Log("MIDI device connected!");
                        else
                            Debug.Log("MIDI device connection failed.");
                    else
                        Debug.Log("A MIDI device is already connected.");
                };

                var onDeviceDisconnect = () => {
                    //TODO check if it IS the MIDI device
                    if (midiDriver != IntPtr.Zero) {
                        Wrapper.delete_fluid_midi_driver(midiDriver);
                        midiDriver = IntPtr.Zero;
                        Debug.Log("MIDI device disconnected.");
                    } else
                        Debug.Log("A non-MIDI device was disconnected.");
                };
                
                //TODO set up listener for device hotplug.
            */
            }
        }

        /*
         * Destructor so we free allocated pointers.
         */
        ~Middleware() {
            ((MiddlewareAPI) this).Cleanup();
        }

        bool MiddlewareAPI.PlayNote(MIDINote midi) {
            return ((MiddlewareAPI) this).PlayNote(midi.chn, midi.key, midi.vel);
        }
        
        bool MiddlewareAPI.PlayNote(int chn, int key, int vel) {
            return Wrapper.fluid_synth_noteon(synth, chn, key, vel) != -1;
        }

        bool MiddlewareAPI.StopNote(MIDINote midi) {
            return ((MiddlewareAPI) this).StopNote(midi.chn, midi.key);
        }
        
        bool MiddlewareAPI.StopNote(int chn, int key) {
            return Wrapper.fluid_synth_noteoff(synth, chn, key) != -1;
        }

        bool MiddlewareAPI.SetGain(float gain) {
            return Wrapper.fluid_settings_setnum
                (settings, "synth.gain", gain) != -1;
        }
        
        bool MiddlewareAPI.SetChannelInstrument
            (int chn, SoundFont sfont, int bnk, int preset) {
            return Wrapper.fluid_synth_program_select
                (synth, chn, sfont.sfont_id, bnk, preset) != -1;
        }

        SoundFont MiddlewareAPI.LoadSoundFont(String streamingAssetQuery) {
            // Get resolved full path
            var sfontPath = new DirectoryInfo(Application.streamingAssetsPath)
                .GetFiles(streamingAssetQuery)[0]
                .ToString();
            // Load SoundFont
            sfont = Wrapper.fluid_synth_sfload(synth, sfontPath, 1);
            ;
           
            if (Util.Check(sfont != -1,
                           "{0}: Failed to load SoundFont: {1}",
                           pkg, sfontPath)) {
                Debug.Log(String.Format("{0}: SoundFont Loaded: {1}",
                                        pkg, sfontPath));
                return new SoundFont(sfont);
            }
            return null;
        }

        bool MiddlewareAPI.ConnectMIDIDevice() {
            // Instantiate MIDI event callback environment
            var midiEnv = GCHandle.Alloc(this);

            var callback = Marshal
                .GetFunctionPointerForDelegate<Wrapper.OnMidiEvent>
                (MIDIDriverCallback);
            
            // Allocate MIDI input driver.
            // Fails if no device is connected (midiDriver == IntPtr.Zero == 0)
            midiDriver = Wrapper.new_fluid_midi_driver
                (settings, callback, (IntPtr) midiEnv);
            Util.Check(midiDriver != IntPtr.Zero,
                       "{0}: Failed to create MIDI driver.", pkg);
            return midiDriver != IntPtr.Zero;
        }

        void MiddlewareAPI.SetOnMIDIDevicePress(OnMIDI onPress, object env) {
            this.onPress = onPress;
            this.onPressEnv = env;
        }

        void MiddlewareAPI.SetOnMIDIDeviceRelease
            (OnMIDI onRelease, object env) {
            this.onRelease = onRelease;
            this.onReleaseEnv = env;
        }

        bool MiddlewareAPI.IsMIDIDeviceConnected() {
            return midiDriver != IntPtr.Zero;
        }

        void MiddlewareAPI.Cleanup() {
            if (midiCallbackEnv.IsAllocated)
                midiCallbackEnv.Free();
            
            if (midiDriver != IntPtr.Zero) {
                Wrapper.delete_fluid_midi_driver(midiDriver);
                midiDriver = IntPtr.Zero;
            }
            
            if (audioDriver != IntPtr.Zero) {
                Wrapper.delete_fluid_audio_driver(audioDriver);
                audioDriver = IntPtr.Zero;
            }

            if (synth != IntPtr.Zero) {
                Wrapper.delete_fluid_synth(synth);
                synth = IntPtr.Zero;
            }

            if (settings != IntPtr.Zero) {
                Wrapper.delete_fluid_settings(settings);
                settings = IntPtr.Zero;
            }
        }

        /*
         * Handles MIDI input processing, user callbacks, and playing sound.
         */
        private void MIDIDriverCallback(IntPtr midiEnvPtr, IntPtr midiEvent) {
            // Dereference environment
            var callbackEnv = ((GCHandle) midiEnvPtr).Target as Middleware;

            // Read MIDI note data
            var midi = Middleware.CreateDeviceMIDINote(midiEvent);
                
            if(midi.typ == MIDINote.TYPE_PRESS) { // If press
                midi = callbackEnv.onPress(midi, callbackEnv.onPressEnv);
                if (midi != null) {
                    ((MiddlewareAPI) callbackEnv).PlayNote(midi);
                    Debug.Log(midi.ToString());
                }
            } else { // If release
                midi = callbackEnv.onRelease(midi, callbackEnv.onReleaseEnv);
                if (midi != null) {
                    ((MiddlewareAPI) callbackEnv).StopNote(midi);
                    Debug.Log(midi.ToString());
                }
            }
        }

        /*
         * Creates a MIDI note handle with MIDI input event data.
         */
        private static MIDINote CreateDeviceMIDINote(IntPtr midiEvent) {
            var midi = new MIDINote() {
                chn = Wrapper.fluid_midi_event_get_channel(midiEvent),
                key = Wrapper.fluid_midi_event_get_key(midiEvent),
                vel = Wrapper.fluid_midi_event_get_velocity(midiEvent),
                ctl = Wrapper.fluid_midi_event_get_control(midiEvent),
                pch = Wrapper.fluid_midi_event_get_pitch(midiEvent),
                pgm = Wrapper.fluid_midi_event_get_program(midiEvent),
                typ = Wrapper.fluid_midi_event_get_type(midiEvent),
                val = Wrapper.fluid_midi_event_get_value(midiEvent)
            };
            Wrapper.fluid_midi_event_get_lyrics(midiEvent, midi.lyr);
            Wrapper.fluid_midi_event_get_text(midiEvent, midi.txt);
            return midi;
        }
    }
}
