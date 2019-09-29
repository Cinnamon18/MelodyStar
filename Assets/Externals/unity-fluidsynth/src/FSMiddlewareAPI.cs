using System;

namespace FluidSynth {
    /*
     * Function signature for processing MIDI inputs.
     * Returning null will keep the note from being played.
     * @param MIDI note to be processed/returned
     * @param arbitary data to be accessible from inside function
     * @return unmodified, modified, or null MIDI note to be played
     */
    public delegate MIDINote OnMIDI(MIDINote midi, object env);

    /*
     * User-available functions to interact with MIDI input and synthesizer.
     */
    public interface MiddlewareAPI {
        /*
         * Start a MIDI note
         * @param MIDI note
         * @return success
         */
        bool PlayNote(MIDINote midi);

        /*
         * Start a MIDI note
         * @param channel
         * @param key
         * @return success
         */
        bool PlayNote(int chn, int key, int vel);

        /*
         * Stop a MIDI note
         * @param MIDI note
         * @return success
         */
        bool StopNote(MIDINote midi);

        /*
         * Stop a MIDI note
         * @param channel
         * @param key
         * @return success
         */
        bool StopNote(int chn, int key);

        /*
         * Assign an instrument program from a bank to a MIDI channel.
         * @param channel
         * @param SoundFont to access instrument from
         * @param bank
         * @param program
         * @return success
         */
        bool SetChannelInstrument
            (int chn, SoundFont sfont, int bnk, int prg);

        /*
         * Load a .sf2 SoundFont2 or .sf3 SoundFont3 file from StreamingAssets.
         * @param Relative path of streaming asset file. Accepts wildcards.
         * @return a handle for the SoundFont file
         */
        SoundFont LoadSoundFont(string streamingAssetPath);

        /*
         * Attempt to connect a plugged-in MIDI device;
         * @return success
         */
        bool ConnectMIDIDevice();

        /*
         * Set the pre-processing callback for when a key is pressed.
         * @param callback function
         * @param accessible dynamic variable for callback
         */
        void SetOnMIDIDevicePress(OnMIDI onPress, object env = null);

        /*
         * Set the pre-processing callback for when a key is released.
         * @param callback function
         * @param accessible dynamic variable for callback
         */
        void SetOnMIDIDeviceRelease(OnMIDI onRelease, object env = null);

        /*
         * Check if a MIDI device to receive input from is connected.
         * @return success
         */
        bool IsMIDIDeviceConnected();
    }
}
