using System.Collections.Generic;
using CustomInput;
using Songs.Model;
using UnityEngine;
using Melanchall.DryWetMidi.MusicTheory;

namespace Songs.Gameplay {
	public class SongManager : MonoBehaviour {

		private Song song;
		public SongSetup songSetup;
		public AudioSource backgroundMusic;
		public AudioSource instrument;

		private List<Lane> lanes;
		private float songStartTime;
		public InputManager input;

		void Start() {
			if (!InputSettings.initalized) {
				InputSettings.setToDefault();
			}
			song = songSetup.readSong("HotCrossBunsLow");

			backgroundMusic.clip = song.backgroundTrack;
			backgroundMusic.Play();
			instrument.clip = song.instrumentSample;


			lanes = songSetup.setupLanes();
			songStartTime = Time.time;
		}

		void Update() {
			createNewNotes();

			//Detect notes being pressed
			senseKeyPresses();
		}

		//Summon new notes as necessary
		private void createNewNotes() {
			List<SongNote> currentNotes = song.getNotesAtTime(Time.time - songStartTime);
			foreach (SongNote note in currentNotes) {
				int offsetFromC = note.toIndex() - new SongNote(NoteName.C, 4).toIndex();
				int noteIndex = offsetFromC + InputSettings.middleC;
				if (noteIndex > 0 && noteIndex < InputSettings.keys.Length - 1) {
					lanes[noteIndex].createNote();
				} else {
					Debug.Log("Note " + note + " outside of playable range");
				}
			}
		}

		private void senseKeyPresses() {
			//sense which keys are pressed
			foreach (int laneIdx in input.keysIndiciesPressedButton) {
				Lane lane = lanes[laneIdx];
				lane.makePressVFx();
				instrument.Play();

				GameObject lowestNote = lane.getLowestNote();
				if (lowestNote != null) {
					float distance = (lowestNote.transform.position - lane.noteTarget.transform.position).magnitude;
					if (distance < 0.5) {
						lane.noteTapVFx(PressAccuracy.Perfect);
					} else if (distance < 1) {
						lane.noteTapVFx(PressAccuracy.Good);
					} else {
						lane.noteTapVFx(PressAccuracy.Miss);
					}
				}
				Destroy(lowestNote);
			}
		}
	}
}