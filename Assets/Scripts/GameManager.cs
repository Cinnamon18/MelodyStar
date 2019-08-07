using System;
using CustomInput;
using UnityEngine;

namespace Gameplay {
	public class GameManager : MonoBehaviour {
		public InputManager inputManager;

		void Start() {
			InputSettings.setToDefault();
		}

		void Update() {
			if (inputManager.keysIndiciesPressedLastFrame.Count > 0) {
				Debug.Log(inputManager.keysIndiciesPressedLastFrame[0]);
			}
		}
	}
}
