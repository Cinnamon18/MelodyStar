using CustomInput;
using UnityEngine;

namespace Song.Gameplay {
	public class SongSetup : MonoBehaviour {

		public GameObject lanePrefab;

		void Start() {
			InputSettings.setToDefault();
			// InputSettings.keys = new int[] { (int)(KeyCode.A), (int)(KeyCode.B), (int)(KeyCode.C) };

			//coooooordinate transforms. but undisciplined.
			float cameraHeight = 10f;
			float lanePrefabWidth = lanePrefab.GetComponent<SpriteRenderer>().size.x;

			float screenToWorld = cameraHeight / Screen.height;
			Vector2 startingPoint = new Vector2(-1 *Screen.width * screenToWorld, cameraHeight) * 0.5f;

			float laneWidth = Screen.width * 1f / InputSettings.keys.Length * screenToWorld;
			for (int i = 0; i < InputSettings.keys.Length; i++) {
				Vector2 horizontalOffset = new Vector2(i * laneWidth + (laneWidth * 0.5f), 0);
				Instantiate(lanePrefab, startingPoint + horizontalOffset, lanePrefab.transform.rotation);
			}
		}
	}
}