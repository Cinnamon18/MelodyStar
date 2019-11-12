using System.Linq;
using CustomInput;
using UnityEngine;

namespace Menuing {
	public class MainMenu : MonoBehaviour {

		void Start() {
			if(PlayerPrefs.HasKey("saved"))
			{
				InputSettings.setToPrefs();
			} else if (!InputSettings.initalized) {
				InputSettings.setToDefault();
			}
		}

		public void story() {
			saveSettings();
			StartCoroutine(SceneTransition.LoadScene("Story"));
		}

		public void freePlay() {
			saveSettings();
			StartCoroutine(SceneTransition.LoadScene("Song"));
		}

		public void configure() {
			saveSettings();
			StartCoroutine(SceneTransition.LoadScene("Configure"));
		}

		public void quit() {
			saveSettings();
			Application.Quit();
		}

		private void saveSettings()
		{
			//TESTING
			//Debug.Log("Saved Settings");
			PlayerPrefs.SetInt("saved", 1);
			PlayerPrefs.SetString("keys", InputSettings.getKeysString());
			PlayerPrefs.SetInt("inputMode", (int) InputSettings.inputMode);
			PlayerPrefs.SetInt("middleC", InputSettings.middleC);
		}

	}
}