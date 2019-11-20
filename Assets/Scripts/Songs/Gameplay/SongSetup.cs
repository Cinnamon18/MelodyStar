using System.Collections.Generic;
using CustomInput;
using Songs.Model;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
namespace Songs.Gameplay {
	public class SongSetup : MonoBehaviour {

		const string basePath = "Assets/Resources/";

		public GameObject lanePrefab;

		public Song readSong(string band, string file) {
			string path = "Songs/" + band + "/" + file + "/";
			List<SongNote> notes = MidiParser.readMidi(basePath + path + "player.mid");
			AudioClip songBackground = Resources.Load<AudioClip>(path + "backing");
			AudioClip hitNoise = Resources.Load<AudioClip>(path + "instrument");
			return new Song(notes, songBackground, hitNoise, getDifficulty(notes));
		}

		public float getDifficulty(List<SongNote> notes) {
			int numnotes = notes.Count;

			List<float> startDifference = new List<float>();
			for (int k = 1; k < numnotes; k++) {
				startDifference.Add(notes[k].startTime - notes[k - 1].startTime);
			}
			float[] output = startDifference.ToArray();
			float sum = 0;
			for (var i = 0; i < output.Length; i++) {
				sum += output[i];
			}
			float average = sum / output.Length;
			int difficulty = Convert.ToInt32(-6 * (Math.Log(average, 10)) + 4.1);
			Debug.Log(difficulty);
			if (difficulty <= 0) {
				difficulty = 1;
			} else if (difficulty > 10) {
				difficulty = 10;
			}
			return difficulty;


		}
		public List<Lane> setupLanes() {
			List<Lane> lanes = new List<Lane>();

			//coooooordinate transforms. but undisciplined.
			float cameraHeight = 10f;
			float lanePrefabWidth = lanePrefab.GetComponent<SpriteRenderer>().size.x;

			float screenToWorld = cameraHeight / Screen.height;
			Vector2 startingPoint = new Vector2(-1 * Screen.width * screenToWorld, cameraHeight) * 0.5f;

			//InputSettings.setToDefaultMidi();

			float laneWidth = Screen.width * 1f / InputSettings.keys.Length * screenToWorld;
			for (int i = 0; i < InputSettings.keys.Length; i++) {
				Vector2 horizontalOffset = new Vector2(i * laneWidth + (laneWidth * 0.5f), 0);
				GameObject lane = Instantiate(lanePrefab, startingPoint + horizontalOffset, lanePrefab.transform.rotation);
				lane.GetComponent<SpriteRenderer>().size = new Vector2(laneWidth, 10);
				lanes.Add(lane.GetComponent<Lane>());
			}

			string[] notes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
			int index = 0;
			for (int j = InputSettings.middleC; j < InputSettings.keys.Length; j++) {
				Lane selectedLane = lanes[j];
				Text noteText = selectedLane.GetComponentInChildren<Text>();
				if (index >= notes.Length) {
					noteText.text = notes[index % notes.Length];
				} else {
					noteText.text = notes[index];
				}
				index += 1;
			}
			/* 	try{
					noteText.text = notes[index];
				}
				catch (ArgumentOutOfRangeException){
					Debug.Log(index);
				} */
			//noteText.text = notes[index];
			//index+=1;

			if (InputSettings.middleC > 0) {
				int count = 0;
				for (int k = InputSettings.middleC - 1; k >= 0; k--) {
					Lane newLane = lanes[k];
					Text noteText2 = newLane.GetComponentInChildren<Text>();
					if ((notes.Length - 1) - count < 0) {
						noteText2.text = notes[Math.Abs((notes.Length - 1) - count % notes.Length)];

					} else {
						noteText2.text = notes[(notes.Length - 1) - count];
					}
					//noteText2.text = notes[(notes.Length-1)-count];
					count += 1;
				}
			}

			return lanes;
		}
		/* Lane firstLane = lanes[0];
		Text firstText = firstLane.GetComponentInChildren<Text>();
		firstText.text = "test";
	 */

	}
}
