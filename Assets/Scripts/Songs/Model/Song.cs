using System.Collections.Generic;
using Songs.Model;
using UnityEngine;
using Songs.Gameplay;

namespace Songs.Model {
	public class Song {
		public List<SongNote> notes;
		public AudioClip backgroundTrack, instrumentSample;
		public float AudioLength;
		public SongManager songManager;

		public Song(List<SongNote> notes, AudioClip backgroundTrack, AudioClip instrumentSample) {
			this.notes = notes;
			this.backgroundTrack = backgroundTrack;
			this.instrumentSample = instrumentSample;
			this.AudioLength = backgroundTrack.length;
		}

		//TODO: Refactor this to be less than O(n)...
		public List<SongNote> getNotesAtTime(float time) {
			List<SongNote> currentNotes = notes.FindAll(note => {
				return !note.created && note.startTime < time && note.endTime > time;
			});
			currentNotes.ForEach(note => { note.created = true;});
			return currentNotes;
		}
		public bool songOver(float time)
		{
			foreach (SongNote currentNote in notes)
			{
				if (time < currentNote.endTime + 5)
				{
					return false;
				}
			}
			return true;
		}
	}
}