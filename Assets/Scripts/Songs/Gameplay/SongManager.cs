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
		public static int numPerfect;
		public static int numGood;
		public static int numMiss;
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
		public int score = 0;
		public int numCorrect = 0;
		PressAccuracy accuracy = PressAccuracy.Miss;
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
						numPerfect++;
						score += 100*scoreMult;
						numCorrect += 1;
					} else if (distance < 1) {
						lane.noteTapVFx(PressAccuracy.Good);
						score += 75*scoreMult;
						numCorrect += 1;
						numGood++;
					}
					else {
						lane.noteTapVFx(PressAccuracy.Miss);
						scoreMult = 0;
						numCorrect = 0;
						numMiss++;
					}
				}

				Destroy(lowestNote);
			}
		}
		public static int Multiplier(int numCorrect)
		{
			if (numCorrect > 200)
			{
				return 8;
			}
			else if (numCorrect > 100)
			{
				return 4;
			}
			else if (numCorrect > 50)
			{
				return 2;
			}
			else
			{
				return 1;
			}

		}







		

		public static int getPerfect()
		{
			return numPerfect;
		}
		public static int getGood()
		{
			return numGood;
		}
		public static int getMiss()
		{
			return numMiss;
		}
	}
}