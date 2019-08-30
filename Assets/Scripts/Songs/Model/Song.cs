using System.Collections.Generic;
using Songs.Model;
using UnityEngine;

namespace Songs.Model {
	public class Song {
		public List<SongNote> notes;

		public Song(List<SongNote> notes) {
			this.notes = notes;
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