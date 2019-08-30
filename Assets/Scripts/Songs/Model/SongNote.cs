using System;
using Melanchall.DryWetMidi.MusicTheory;
using UnityEngine;

namespace Songs.Model {
	//This class is named SongNote to avoid conflicts with DryWetMidi's two `Note` classes. I figured it'd confuse ppl in imports.
	public class SongNote {
		public static readonly int numNoteNames = Enum.GetNames(typeof(NoteName)).Length;
		//in midi, a0 = 21. in drywetmidi, a = 9 so we'll offset by 12 so that they match up
		const int dryWetMidiOffset = 12;

		public NoteName letter;
		public int key;
		//Times are in whole seconds
		public float startTime, endTime;
		public bool created, pressed;

		public SongNote(NoteName letter, int key) :
			this(letter, key, 0, 1) { }

		public SongNote(NoteName letter, int key, float startTime, float endTime) {
			this.letter = letter;
			this.key = key;
			this.startTime = startTime;
			this.endTime = endTime;
		}

		public int toIndex() {
			return (int)(this.letter) + dryWetMidiOffset + key * (numNoteNames);
		}

		public static SongNote noteFromIndex(int index) {
			if (index < 0) {
				return null;
			}
			index = index - dryWetMidiOffset;
			return new SongNote((NoteName)(index % numNoteNames), (index / numNoteNames));
		}

		public override string ToString() {
			return "" + ((NoteName)(letter)).ToString() + key + " (" + startTime + ", " + endTime + ")";
		}
	}
}