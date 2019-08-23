using System.Linq;
using UnityEngine;

namespace CustomInput {
	public static class InputSettings {
		//Alas, array 'literals' are not truly literal and thus not compile time constants!
		public static readonly int[] defaultKeys = { (int)(KeyCode.A), (int)(KeyCode.S), (int)(KeyCode.D), (int)(KeyCode.F), (int)(KeyCode.G), (int)(KeyCode.H), (int)(KeyCode.J), (int)(KeyCode.K), (int)(KeyCode.L), (int)(KeyCode.Semicolon), (int)(KeyCode.Quote) };
		public static readonly int[] defaultMidiKeys = Enumerable.Range(0, 128).ToArray();
		public static readonly InputMode defaultInputMode = InputMode.Keyboard;
		public static readonly int defaultMiddleC = defaultKeys.Length / 2;

		public static int[] keys;
		public static InputMode inputMode;
		public static int middleC;

		public static void setToDefault() {
			InputSettings.keys = defaultKeys;
			InputSettings.inputMode = defaultInputMode;
			InputSettings.middleC = defaultMiddleC;
		}

		public static void setToDefaultMidi() {
			InputSettings.keys = defaultMidiKeys;
			InputSettings.inputMode = InputMode.MIDI;
			InputSettings.middleC = defaultMiddleC;
		}
	}
}