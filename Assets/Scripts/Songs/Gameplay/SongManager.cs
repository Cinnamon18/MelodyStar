using System.Collections.Generic;
using CustomInput;
using Songs.Model;
using UnityEngine;
using Melanchall.DryWetMidi.MusicTheory;
using Songs.Gameplay;

namespace Songs.Gameplay {
	public class SongManager : MonoBehaviour {

		public Song song;
		public SongSetup songSetup;
		public AudioSource backgroundMusic;
		public AudioSource instrument;
		public int numPerfect;
		public int numGood;
		public int numMiss;
		public List<Lane> lanes;
		public float songStartTime;
		public InputManager input;
		public string bandName;
		public string songName;

		void Start() {
			if (!InputSettings.initalized)
			{
				InputSettings.setToDefault();
			}
			//bandName = "Test";
			//songName = "HotCrossBunsLow";
			song = songSetup.readSong(bandName, songName);

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

		[HideInInspector]
		public int score = 0;
		[HideInInspector]
		public int numCorrect = 0;
		[HideInInspector]
		public int highestCombo = 0;
		private void senseKeyPresses() {
			//sense which k_eys are pressed
			foreach (int laneIdx in input.keysIndiciesPressedButton) {
				Lane lane = lanes[laneIdx];
				lane.makePressVFx();
				instrument.Play();
				GameObject lowestNote = lane.getLowestNote();

				int scoreMult = Multiplier(numCorrect);

				if (lowestNote != null) {
					float distance = (lowestNote.transform.position - lane.noteTarget.transform.position).magnitude;
					if (distance < 0.5) {
						lane.noteTapVFx(PressAccuracy.Perfect);
						score += 100 * scoreMult;
						numCorrect += 1;
						numPerfect++;
					} else if (distance < 1) {
						lane.noteTapVFx(PressAccuracy.Good);
						score += 75 * scoreMult;
						numCorrect += 1;
						numGood++;
					}
					else {
						lane.noteTapVFx(PressAccuracy.Miss);
						scoreMult = 0;
						numCorrect = 0;
						numMiss++;
					}
					if (numCorrect > highestCombo)
					{
						highestCombo = numCorrect;
					}
				}

				Destroy(lowestNote);
			}
		}
		public static int Multiplier(int numCorrect) {
			if (numCorrect > 200) {
				return 8;
			} else if (numCorrect > 100) {
				return 4;
			} else if (numCorrect > 50) {
				return 2;
			} else {
				return 1;
			}
		}
	}
}