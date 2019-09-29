using System;
using System.Runtime.InteropServices;

namespace FluidSynth {
    /*
     * Class for containing imported C functions.
     */
    public static class Wrapper {
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Allocate fluid_settings_t*.
         * @return allocated or null pointer
         */
        public static extern IntPtr new_fluid_settings();
    
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Allocates fluid_synth_t*.
         * @param allocated settings : fluid_settings_t*
         * @return allocated or null pointer
         */
        public static extern IntPtr new_fluid_synth(IntPtr settings);
    
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Allocates fluid_audio_driver_t*.
         * @param allocated settings : fluid_settings_t*
         * @return allocated or null pointer
         */
        public static extern IntPtr new_fluid_audio_driver
            (IntPtr settings, IntPtr synth);

        /*
         * Function signature for MIDI input event callback.
         * @param arbitrary data to be available inside function
         * @param allocated pointer : fluid_midi_event_t*
         */
        public delegate void OnMidiEvent(IntPtr handleData, IntPtr midiEvent);
        
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Allocates fluid_midi_driver_t*.
         * @param allocated settings : fluid_settings_t*
         * @param function pointer for MIDI input event callback : OnMidiEvent
         * @param pointer to arbitrary dynamic data to be accessed in handle
         * @return allocated or null pointer
         */
        public static extern IntPtr new_fluid_midi_driver
            (IntPtr settings, IntPtr handle, IntPtr handleData);
        
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Loads a SoundFont2 or SoundFont3 file.
         * @param allocated synth : flid_synth_t*
         * @param full path to soundfont file
         * @param 1 to reset instrument presets, else 0
         * @return -1 if failed, else 0
         */
        public static extern int fluid_synth_sfload
            (IntPtr synth, String sfontPath, int resetPresets);
    
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Plays a MIDI note.
         * @param allocated synth : flid_synth_t*
         * @param MIDI channel
         * @param MIDI key
         * @param MIDI velocity
         * @return -1 if failed, else 0
         */
        public static extern int fluid_synth_noteon
            (IntPtr synth, int chn, int key, int vel);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Stops a MIDI note.
         * @param allocated synth : flid_synth_t*
         * @param MIDI channel
         * @param MIDI key
         * @return -1 if failed, else 0
         */
        public static extern int fluid_synth_noteoff
            (IntPtr synth, int chn, int key);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Sets a channel to instrument preset on specified bank.
         * @param allocated synth : flid_synth_t*
         * @param MIDI channel
         * @param soundfont ID
         * @param MIDI bank
         * @param MIDI preset
         * @return -1 if failed, else 0
         */
        public static extern int fluid_synth_program_select
            (IntPtr synth, int chn, int sfont, int bnk, int preset);
        
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Copy audio output buffer to external buffer.
         * @param allocated synth : flid_synth_t*
         * @param left channel array
         * @param left channel index offset
         * @param left channel index increment
         * @param right channel array
         * @param right channel index offset
         * @param right channel index increment
         * @return -1 if failed, else 0
         */
        public static extern int fluid_synth_write_float
            (IntPtr synth, int len, float[] lout, int loff, int lincr,
             float[] rout, int roff, int rincr);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Frees allocated fluid_midi_driver_t*.
         * @param allocated pointer : fluid_midi_driver_t*
         */
        public static extern void delete_fluid_midi_driver(IntPtr midiDriver);
        
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Frees allocated fluid_auido_driver_t*.
         * @param allocated pointer : fluid_audio_driver_t*
         */
        public static extern void delete_fluid_audio_driver(IntPtr audioDriver);
    
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Frees allocated fluid_synth_t*.
         * @param allocated pointer : fluid_synth_t*
         */
        public static extern void delete_fluid_synth(IntPtr synth);
    
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Frees allocated fluid_settings_t*.
         * @param allocated pointer : fluid_settings_t*
         */
        public static extern void delete_fluid_settings(IntPtr settings);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Set double float value of a FluidSynth settings parameter.
         * @param allocated pointer : fluid_settings_t*
         * @param parameter name
         * @param parameter value
         * @return -1 if failed, else 0
         */
        public static extern int fluid_settings_setnum
            (IntPtr settings, String name, double val);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Set integer value of a FluidSynth settings parameter.
         * @param allocated pointer : fluid_settings_t*
         * @param parameter name
         * @param parameter value
         * @return -1 if failed, else 0
         */
        public static extern int fluid_settings_setint
            (IntPtr settings, String name, int val);
        
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Set string value of a FluidSynth settings parameter.
         * @param allocated pointer : fluid_settings_t*
         * @param parameter name
         * @param parameter value
         * @return -1 if failed, else 0
         */
        public static extern int fluid_settings_setstr
            (IntPtr settings, String name, String val);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the channel of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @return MIDI channel
         */
        public static extern int fluid_midi_event_get_channel
            (IntPtr midiEvent);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the key of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @return MIDI key
         */
        public static extern int fluid_midi_event_get_key
            (IntPtr midiEvent);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the velocity of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @return MIDI velocity
         */
        public static extern int fluid_midi_event_get_velocity
            (IntPtr midiEvent);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the control of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @return MIDI control
         */
        public static extern int fluid_midi_event_get_control(IntPtr midiEvent);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the lyrics of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @param string var to copy lyrics into
         * @param copied string length
         * @return -1 if failed, else 0
         */
        public static extern int fluid_midi_event_get_lyrics
            (IntPtr midiEvent, String lyrics, int size = 0);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the text of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @param string var to copy text into
         * @param copied string length
         * @return -1 if failed, else 0
         */
        public static extern int fluid_midi_event_get_text
            (IntPtr midiEvent, String text, int size = 0);
        
        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the pitch of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @return MIDI pitch
         */
        public static extern int fluid_midi_event_get_pitch(IntPtr midiEvent);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the program of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @return MIDI program
         */
        public static extern int fluid_midi_event_get_program(IntPtr midiEvent);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the type of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @return MIDI type
         */
        public static extern int fluid_midi_event_get_type(IntPtr midiEvent);

        [DllImport("libfluidsynth-2.dll")]
        /*
         * Get the value of a MIDI note event
         * @param allocated pointer : fluid_midi_event_t*
         * @return MIDI value
         */
        public static extern int fluid_midi_event_get_value(IntPtr midiEvent);
    }
}
