using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomInput {
	public class InputConfiguration : MonoBehaviour {

		public Transform canvas;
		public GameObject whiteKeyPrefab, blackKeyPrefab;
		public const float cameraSize = 10;
		List<GameObject> keys = new List<GameObject>();

		int keyCount = 0;

		void Start() {
			InputSettings.setToDefault();
			keyCount = InputSettings.keys.Count();
			createNewKeys();
		}

		void Update() {
			//If scroll wheel
			if (Input.GetAxis("MouseScrollWheel") != 0) {
				keyCount += (int)(Input.GetAxis("MouseScrollWheel"));
			}
		}

		private void createNewKeys() {
			foreach (GameObject key in keys) {
				Destroy(key);
			}
			keys.Clear();

			for (int i = 0; i < InputSettings.keys.Length; i++) {
				GameObject whiteKey = Object.Instantiate(whiteKeyPrefab, canvas);
				RectTransform whiteRect = whiteKey.GetComponent<RectTransform>();
				whiteRect.anchorMin = new Vector2((float)(i) / (InputSettings.keys.Length), 0);
				whiteRect.anchorMax = new Vector2((float)(i + 1) / (InputSettings.keys.Length), 1);
				keys.Add(whiteKey);
			}
			for (int i = 0; i < InputSettings.keys.Length - 1; i++) {
				GameObject blackKey = Object.Instantiate(blackKeyPrefab, canvas);
				RectTransform blackRect = blackKey.GetComponent<RectTransform>();
				blackRect.anchorMin = new Vector2((float)(i + 0.7) / (InputSettings.keys.Length), 0.2f);
				blackRect.anchorMax = new Vector2((float)(i + 1.3) / (InputSettings.keys.Length), 1);
				keys.Add(blackKey);
			}
		}


		public void setOffset(float offset) { InputSettings.offset = offset; }

		public void setWidth(float width) { InputSettings.keyWidth = width; }

		public void setMiddleC(int middleC) { InputSettings.middleC = middleC; }

		public void setMidiRange(int min, int max) { InputSettings.keys = Enumerable.Range(min, max - min).ToArray(); }

	}

}