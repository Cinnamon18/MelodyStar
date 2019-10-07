//Does unity have it's own input system? Yeah! But we've gotta mix in the external MIDI package.
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using System.Collections;

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

		public IEnumerator awaitKeyPress(System.Action<int> callback, List<int> keysToIgnore = null) {
			while (this.keysIndiciesPressed.Count == 0 ||
				(keysToIgnore?.Contains(this.keysIndiciesPressed[0]) ?? false)) {
				yield return null;
			}
			callback(this.keysIndiciesPressed[0]);
		}

		public IEnumerator awaitKeyPressRaw(System.Action<int> callback, float delay = 0) {
			yield return new WaitForSeconds(delay);
			int keyPressed = -1;
			while (keyPressed == -1) {
				for (int i = 0; i < 128; i++) {
					if (MidiMaster.GetKey(i) > 0.1f) {
						keyPressed = i;
					}
				}
				if (keyPressed == -1) {
					yield return null;
				}
			}
			callback(keyPressed);
		}
	}
}
