using System;
using CustomInput;
using MidiJack;
using UnityEngine;

namespace Gameplay {
	public class GameManager : MonoBehaviour {
		public InputManager inputManager;

		void Start() {
		}

		void Update() {
			if (inputManager.keysIndiciesPressed.Count > 0) {
				Debug.Log(inputManager.keysIndiciesPressed[0]);
			}
		}
	}
}
