using System.Linq;
using UnityEngine;

namespace CustomInput {
	public static class InputSettings {
		//Alas, array 'literals' are not truly literal and thus not compile time constants!
		public static readonly int[] defaultKeys = { (int)(KeyCode.A), (int)(KeyCode.S), (int)(KeyCode.D), (int)(KeyCode.F), (int)(KeyCode.G), (int)(KeyCode.H), (int)(KeyCode.J), (int)(KeyCode.K), (int)(KeyCode.L), (int)(KeyCode.Semicolon) };
		public static readonly int defaultMiddleC = 4;
		public static readonly InputMode defaultInputMode = InputMode.Keyboard;

		public static readonly int[] defaultMidiKeys = Enumerable.Range(21, 100).ToArray();
		public static readonly int defaultMiddleCMidi = 60 - 21; // 60 is C in midi!

		public static int[] keys;
		public static InputMode inputMode;
		public static int middleC; //index in the keys array corresponding to middle C.

		public static bool initalized = false;

		public static void setToDefault() {
			InputSettings.keys = defaultKeys;
			InputSettings.inputMode = defaultInputMode;
			InputSettings.middleC = defaultMiddleC;
			initalized = true;
		}

		public static void setToDefaultMidi() {
			InputSettings.keys = defaultMidiKeys;
			InputSettings.inputMode = InputMode.MIDI;
			InputSettings.middleC = defaultMiddleCMidi;
			initalized = true;
		}
	}
}