using System;
using CustomInput;
using UnityEngine;

namespace Gameplay {
	public class GameManager : MonoBehaviour {
		// Start is called before the first frame update
		public InputManager inputManager;

		void Start() {
			InputSettings.setToDefault();
		}

		// Update is called once per frame
		void Update() {
			if (inputManager.keysIndiciesPressedLastFrame.Count > 0) {
				Debug.Log(inputManager.keysIndiciesPressedLastFrame[0]);
			}
		}
	}
}
