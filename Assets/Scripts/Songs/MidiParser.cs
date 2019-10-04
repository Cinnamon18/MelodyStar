using UnityEngine;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using Songs.Model;
using System.Collections.Generic;

namespace Songs {
	public class MidiParser {

		public static List<SongNote> readMidi(string fileName) {
			var midiFile = MidiFile.Read(fileName);

			List<SongNote> notes = new List<SongNote>();
			TempoMap tempoMap = midiFile.GetTempoMap();
			foreach (var trackChunk in midiFile.GetTrackChunks()) {
				using (var notesManager = trackChunk.ManageNotes()) {
					foreach (Melanchall.DryWetMidi.Smf.Interaction.Note midiNote in notesManager.Notes) {
						SongNote note = new SongNote(midiNote.NoteName, midiNote.Octave, midiNote.Time / 1000f, (midiNote.Time + midiNote.Length) / 1000f);
						notes.Add(note);
					}
				}
			}

			return notes;
		}
	}
}