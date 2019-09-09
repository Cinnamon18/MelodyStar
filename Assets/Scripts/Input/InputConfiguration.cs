using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MidiJack;
using UnityEngine;
using UnityEngine.UI;

namespace CustomInput {
	public class InputConfiguration : MonoBehaviour {
		public GameObject keyboardInputUI, mIDIInputUI, middleCLabel, middleCButt, midiRangeLabel, midiRangeButt;
		public InputManager inputManager;

		void Start() {
			//Just for testing
			// InputSettings.setToDefault();
			InputSettings.setToDefaultMidi();

			if (InputSettings.inputMode == InputMode.Keyboard) {
				setInputModeKeyboard();
			} else if (InputSettings.inputMode == InputMode.MIDI) {
				setInputModeMidi();
			}
		}

		public void setInputModeKeyboard() {
			InputSettings.inputMode = InputMode.Keyboard;
			InputSettings.keys = InputSettings.defaultKeys;
			keyboardInputUI.SetActive(true);
			mIDIInputUI.SetActive(false);
			midiRangeLabel.SetActive(false);
			midiRangeButt.SetActive(false);
		}

		public void setInputModeMidi() {
			InputSettings.inputMode = InputMode.MIDI;
			keyboardInputUI.SetActive(false);
			mIDIInputUI.SetActive(true);
			midiRangeLabel.SetActive(true);
			midiRangeButt.SetActive(true);
		}

		private IEnumerator awaitKeyPress(System.Action<int> callback, List<int> keysToIgnore = null) {
			while (inputManager.keysIndiciesPressed.Count == 0 ||
				(keysToIgnore?.Contains(inputManager.keysIndiciesPressed[0]) ?? false)) {
				yield return null;
			}
			callback(inputManager.keysIndiciesPressed[0]);
		}

		private IEnumerator awaitKeyPressRaw(System.Action<int> callback, float delay = 0) {
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



		//Callback pyramid this feels like i'm writing pre es6 js sigh
		public void startMidiConfig() {
			midiRangeButt.SetActive(false);
			midiRangeLabel.GetComponentInChildren<Text>().text = "Press the lowest key...";
			StartCoroutine(awaitKeyPressRaw((int minKey) => {
				midiRangeLabel.GetComponentInChildren<Text>().text = "Press the highest key...";
				StartCoroutine(awaitKeyPressRaw((int maxKey) => {
					InputSettings.keys = Enumerable.Range(minKey, maxKey - minKey + 1).ToArray();
					midiRangeLabel.GetComponentInChildren<Text>().text = "Set Midi Range";
					midiRangeButt.SetActive(true);
				}, 0.5f));
			}));
		}

		public void mainMenu() {
			StartCoroutine(SceneTransition.LoadScene("MainMenu"));
		}
	}
}