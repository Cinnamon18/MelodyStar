using UnityEngine;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;

namespace Song {
	public class MidiParser {

		public static void readMidi() {
			var midiFile = MidiFile.Read("Assets/Songs/StartDash.mid");

			TempoMap tempoMap = midiFile.GetTempoMap();
			Debug.Log(midiFile.GetTimedEvents());
		}

	}
}