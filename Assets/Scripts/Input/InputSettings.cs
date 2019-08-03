using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomInput {
	public static class InputSettings {
		public static readonly int[] defaultKeys = { (int)(KeyCode.A), (int)(KeyCode.S), (int)(KeyCode.D), (int)(KeyCode.F), (int)(KeyCode.G), (int)(KeyCode.H), (int)(KeyCode.J), (int)(KeyCode.K), (int)(KeyCode.L), (int)(KeyCode.Semicolon), (int)(KeyCode.Quote) };
		public static readonly float defaultKeyWidth = 1f;
		public static readonly InputMode defaultInputMode = InputMode.Keyboard;

		public static int[] keys;
		public static float keyWidth;
		public static InputMode inputMode;

		public static void setToDefault() {
			InputSettings.keys = defaultKeys;
			InputSettings.keyWidth = defaultKeyWidth;
			InputSettings.inputMode = defaultInputMode;
		}
	}
}