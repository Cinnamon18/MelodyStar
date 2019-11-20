using System;

namespace FluidSynth {
    /*
     * Class for soundfont ID wrapping.
     */
    public class SoundFont {
        public int sfont_id = 0;
        public SoundFont(int sfont_id) {
            this.sfont_id = sfont_id;
        }
    }
}
