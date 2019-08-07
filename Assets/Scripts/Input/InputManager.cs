//Does unity have it's own input system? Yeah! But we've gotta mix in the external MIDI package.
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

namespace CustomInput {
	public class InputManager : MonoBehaviour {
		private const float noteActivationThreshold = 0.1f;

		//keys contains ints which correspond to a given midi key, keyboard key, etc.
		[HideInInspector]
		public int[] keys;
		//keysIndiciesPressedLastFrame indicates the index of the key in the above array that was pressed last frame.
		//done this way because the gameplay will only care about the index of the key pressed, not the actual key. gotta abstract.
		[HideInInspector]
		public List<int> keysIndiciesPressedLastFrame = new List<int>();

		void Start() {
			keys = InputSettings.keys;
		}

		void Update() {
			keysIndiciesPressedLastFrame.Clear();
			for (int i = 0; i < keys.Length; i++) {
				if (InputSettings.inputMode == InputMode.Keyboard) {
					if (Input.GetKey((KeyCode)(keys[i]))) {
						keysIndiciesPressedLastFrame.Add(i);
					}
				} else if (InputSettings.inputMode == InputMode.MIDI) {
					if (MidiMaster.GetKey(keys[i]) > noteActivationThreshold) {
						keysIndiciesPressedLastFrame.Add(i);
					}
				}
			}
		}
	}
}
