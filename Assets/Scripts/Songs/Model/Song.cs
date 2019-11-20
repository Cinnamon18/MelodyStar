using System.Collections.Generic;
using Songs.Model;
using UnityEngine;

namespace Songs.Model {
	public class Song {
		public List<SongNote> notes;
		public AudioClip backgroundTrack, instrumentSample;
		
		public float difficulty;
		public Song(List<SongNote> notes, AudioClip backgroundTrack, AudioClip instrumentSample ,float difficulty) {
			this.notes = notes;
			this.backgroundTrack = backgroundTrack;
			this.instrumentSample = instrumentSample;
			this.difficulty = difficulty;

		}

		//TODO: Refactor this to be less than O(n)...
		public List<SongNote> getNotesAtTime(float time) {
			List<SongNote> currentNotes = notes.FindAll(note => {
				return !note.created && note.startTime < time && note.endTime > time;
			});
			currentNotes.ForEach(note => { note.created = true;});
			return currentNotes;
		}

	}
}