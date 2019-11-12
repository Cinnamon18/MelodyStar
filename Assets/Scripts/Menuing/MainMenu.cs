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
			StartCoroutine(SceneTransition.LoadScene("Story"));
		}

		public void freePlay() {
			StartCoroutine(SceneTransition.LoadScene("Song"));
		}

		public void configure() {
			StartCoroutine(SceneTransition.LoadScene("Configure"));
		}

		public void quit() {
			Application.Quit();
		}



	}
}