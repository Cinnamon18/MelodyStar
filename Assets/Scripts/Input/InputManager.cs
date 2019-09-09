//Does unity have it's own input system? Yeah! But we've gotta mix in the external MIDI package.
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

namespace CustomInput {
	public class InputManager : MonoBehaviour {
		private const float noteActivationThreshold = 0.1f;

		//keysIndiciesPressed indicates the index of the key in the above array that was pressed last frame.
		//done this way because the gameplay will only care about the index of the key pressed, not the actual key. gotta abstract.
		[HideInInspector]
		public List<int> keysIndiciesPressed = new List<int>();

		//MidiMaster doesn't seem to let us wait for key up. This list waits for key up before registering a key again.
		[HideInInspector]
		public List<int> keysIndiciesPressedButton = new List<int>();
		private List<int> keysIndiciesPressedLastFrame = new List<int>();

		void Update() {
			keysIndiciesPressed.Clear();
			keysIndiciesPressedButton.Clear();
			for (int i = 0; i < InputSettings.keys.Length; i++) {
				if (InputSettings.inputMode == InputMode.Keyboard) {
					if (Input.GetKey((KeyCode)(InputSettings.keys[i]))) {
						keysIndiciesPressed.Add(i);

						if (!keysIndiciesPressedLastFrame.Contains(i)) {
							keysIndiciesPressedButton.Add(i);
						}
					}
				} else if (InputSettings.inputMode == InputMode.MIDI) {
					if (MidiMaster.GetKey(InputSettings.keys[i]) > noteActivationThreshold) {
						keysIndiciesPressed.Add(i);

						if (!keysIndiciesPressedLastFrame.Contains(i)) {
							keysIndiciesPressedButton.Add(i);
						}
					}
				}
			}
			keysIndiciesPressedLastFrame = new List<int>(keysIndiciesPressed);
		}
	}
}
