using System.Collections.Generic;
using CustomInput;
using Songs.Model;
using UnityEngine;
using Melanchall.DryWetMidi.MusicTheory;

namespace Songs.Gameplay {
	public class SongManager : MonoBehaviour {

		private Song song;
		private List<Lane> lanes;
		private float songStartTime;

		void Start() {
			InputSettings.setToDefault();
			// InputSettings.setToDefaultMidi();

			song = MidiParser.readMidi("Assets/Songs/HotCrossBunsLow.mid");
			SongSetup songSetup = gameObject.GetComponent<SongSetup>();
			lanes = songSetup.setupLanes();
			songStartTime = Time.time;
		}

		void Update() {
			createNewNotes();

			//Detect notes being pressed

		}

		//Summon new notes as necessary
		private void createNewNotes() {
			List<SongNote> currentNotes = song.getNotesAtTime(Time.time);
			foreach (SongNote note in currentNotes) {
				int offsetFromC = note.toIndex() - new SongNote(NoteName.C, 4).toIndex();
				int noteIndex = offsetFromC + InputSettings.middleC;
				if (noteIndex > 0 && noteIndex < InputSettings.keys.Length - 1) {
					lanes[noteIndex].createNote();
				}
			}
		}
	}
}