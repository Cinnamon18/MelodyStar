using System;

namespace FluidSynth {
    /*
     * Class for MIDI note handling.
     */
    public class MIDINote {
        public int
            chn = -1, // Channel
            key = -1, // Key
            vel = -1, // Velocity
            ctl = -1, // Control
            pch = -1, // Pitch
            pgm = -1, // Program
            typ = -1, // Type
            val = -1; // Value
        public string
            lyr = "", // Lyrics
            txt = ""; // String
        /*
         * DIY MIDI note for use with an initializer list.
         */
        public MIDINote() {}
        
        /*
         * Your basic anonymous MIDI note.
         * @param channel
         * @param key
         * @param velocity
         * @param lyrics
         * @param text
         */
        public MIDINote(int chn, int key, int vel,
                        String lyr = "", String txt = "") {
            this.chn = chn;
            this.key = key;
            this.vel = vel;
            this.lyr = lyr;
            this.txt =txt;
        }

        public override String ToString() {
            const String fmt
                = "chn: {0}, key: {1}, vel: {2}, ctl: {3}, pch: {4}, pgm: {5}, "
                + "typ: {6}, val: {7}, lyr: {8}, txt: {9}";
            return String.Format
                (fmt, chn, key, vel, ctl, pch, pgm, typ, val, lyr, txt);
        }
    }
}
