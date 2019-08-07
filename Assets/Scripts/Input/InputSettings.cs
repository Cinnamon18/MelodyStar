using UnityEngine;

namespace CustomInput {
	public static class InputSettings {
		private static readonly int[] defaultKeys = { (int)(KeyCode.A), (int)(KeyCode.S), (int)(KeyCode.D), (int)(KeyCode.F), (int)(KeyCode.G), (int)(KeyCode.H), (int)(KeyCode.J), (int)(KeyCode.K), (int)(KeyCode.L), (int)(KeyCode.Semicolon), (int)(KeyCode.Quote) };
		private static readonly InputMode defaultInputMode = InputMode.Keyboard;
		private static readonly float defaultKeyWidth = 1f;
		private static readonly float defaultOffset = 0f;
		private static readonly int defaultMiddleC = (int)(KeyCode.H);

		public static int[] keys;
		public static float keyWidth;
		public static InputMode inputMode;
		public static float offset;
		public static int middleC;

		public static void setToDefault() {
			InputSettings.keys = defaultKeys;
			InputSettings.keyWidth = defaultKeyWidth;
			InputSettings.inputMode = defaultInputMode;
			InputSettings.offset = defaultOffset;
			InputSettings.middleC = defaultMiddleC;
		}
	}
}