using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			Debug.Log(InputSettings.keys.Length);

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


		public void setMiddleC() {
			middleCLabel.GetComponentInChildren<Text>().text = "Press key...";
			middleCButt.GetComponent<Button>().interactable = false;
			StartCoroutine(awaitKeyPress((int key) => {
				InputSettings.middleC = key;
				middleCLabel.GetComponentInChildren<Text>().text = "Middle C set to: " + InputSettings.middleC;
				StartCoroutine(restoreMiddleC());
			}));
		}

		public IEnumerator restoreMiddleC() {
			yield return new WaitForSeconds(2f);
			middleCLabel.GetComponentInChildren<Text>().text = "Set Middle C";
			middleCButt.GetComponent<Button>().interactable = true;
		}

		private IEnumerator awaitKeyPress(System.Action<int> callback, List<int> keysToIgnore = null) {
			while (inputManager.keysIndiciesPressed.Count == 0 ||
				(keysToIgnore?.Contains(inputManager.keysIndiciesPressed[0]) ?? false)) {
				yield return null;
			}
			callback(inputManager.keysIndiciesPressed[0]);
		}



		//Callback pyramid this feels like i'm writing pre es6 js sigh
		public void startMidiConfig() {
			midiRangeButt.SetActive(false);
			midiRangeLabel.GetComponentInChildren<Text>().text = "Press the lowest key...";
			StartCoroutine(awaitKeyPress((int minKey) => {
				midiRangeLabel.GetComponentInChildren<Text>().text = "Press the highest key...";
				StartCoroutine(awaitKeyPress((int maxKey) => {
					InputSettings.keys = Enumerable.Range(minKey, maxKey - minKey).ToArray();
					midiRangeLabel.GetComponentInChildren<Text>().text = "Set Midi Range";
					midiRangeButt.SetActive(true);
				}, new List<int> { minKey }));
			}));
		}

		public void mainMenu() {
			StartCoroutine(SceneTransition.LoadScene("MainMenu"));
		}
	}
}