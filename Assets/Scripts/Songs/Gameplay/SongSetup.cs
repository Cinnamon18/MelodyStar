using System.Collections.Generic;
using CustomInput;
using Songs.Model;
using UnityEngine;

namespace Songs.Gameplay {
	public class SongSetup : MonoBehaviour {

		const string basePath = "Assets/Resources/";

		public GameObject lanePrefab;

		public Song readSong(string file) {
			string path = "Songs/" + file + "/";
			List<SongNote> notes = MidiParser.readMidi(basePath + path + "notes.mid");
			AudioClip songBackground = Resources.Load<AudioClip>(path + "background");
			AudioClip hitNoise = Resources.Load<AudioClip>(path + "instrument");

			return new Song(notes, songBackground, hitNoise);
		}

		public List<Lane> setupLanes() {
			List<Lane> lanes = new List<Lane>();

			//coooooordinate transforms. but undisciplined.
			float cameraHeight = 10f;
			float lanePrefabWidth = lanePrefab.GetComponent<SpriteRenderer>().size.x;

			float screenToWorld = cameraHeight / Screen.height;
			Vector2 startingPoint = new Vector2(-1 * Screen.width * screenToWorld, cameraHeight) * 0.5f;

			float laneWidth = Screen.width * 1f / InputSettings.keys.Length * screenToWorld;
			for (int i = 0; i < InputSettings.keys.Length; i++) {
				Vector2 horizontalOffset = new Vector2(i * laneWidth + (laneWidth * 0.5f), 0);
				GameObject lane = Instantiate(lanePrefab, startingPoint + horizontalOffset, lanePrefab.transform.rotation);
				lane.GetComponent<SpriteRenderer>().size = new Vector2(laneWidth, 10);
				lanes.Add(lane.GetComponent<Lane>());
			}

			return lanes;
		}
	}
}