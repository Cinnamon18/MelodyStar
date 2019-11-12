using System;
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

		public static void setToPrefs()
		{
			//TESTING
			//Debug.Log("Keys: " + PlayerPrefs.GetString("keys"));
			//Debug.Log("Input Mode: " + PlayerPrefs.GetInt("inputMode"));
			//Debug.Log("Middle C: " + PlayerPrefs.GetInt("middleC"));
			InputSettings.keys = parseKeysString(PlayerPrefs.GetString("keys"));
			InputSettings.inputMode = (InputMode) PlayerPrefs.GetInt("inputMode");
			InputSettings.middleC = PlayerPrefs.GetInt("middleC");
		}

		public static string getKeysString()
		{
			return getKeysString(keys);
		}

		public static string getKeysString(int[] keys)
		{
			return string.Join(",", keys);
		}

		public static int[] parseKeysString(string keysString)
		{
			string[] elements = keysString.Split(',');

			int[] result = new int[elements.Length];
			for (int i = 0; i < result.Length; i++)
			{
				try
				{
					result[i] = Int32.Parse(elements[i]);
				} catch (Exception e)
				{
					Debug.LogWarning("Key Settings were not parsed successfully (ya done f'ed up).\n Using defaults instead.");
					Debug.Log(e.StackTrace);

					setToDefault();
				}
			}

			return result;
		}
	}
}