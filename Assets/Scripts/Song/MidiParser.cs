using Melanchall.DryWetMidi;
using Melanchall.DryWetMidi.Smf;

namespace Song {
	public class MidiParser {

		void readMidi() {
			var midiFile = MidiFile.Read("../../../Songs/StartDash.midi");
		}

	}
}