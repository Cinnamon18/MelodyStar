using System.Linq;
using CustomInput;
using UnityEngine;
using GameLoop;

namespace Menuing {
	public class MainMenu : MonoBehaviour {

		public GameLoopManager gameLoopManager;

		void Start() {
			if (PlayerPrefs.HasKey("saved")) {
				InputSettings.setToPrefs();
			} else if (!InputSettings.initalized) {
				InputSettings.setToDefault();
			}

			GameLoopManager.gameLoop = GameLoopManager.defaultGameLoop;
		}

		public void story() {
			StartCoroutine(gameLoopManager.advance());
		}

		public void freePlay() {
			StartCoroutine(SceneTransition.LoadScene("SongSelect"));
		}

		public void configure() {
			StartCoroutine(SceneTransition.LoadScene("Configure"));
		}

		public void credits() {
			StartCoroutine(SceneTransition.LoadScene("Credits"));
		}

		public void quit() {
			Application.Quit();
		}
	}
}