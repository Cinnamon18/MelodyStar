using System.Linq;
using UnityEngine;

namespace Menuing {
	public class MainMenu : MonoBehaviour {
		public void story() {
			StartCoroutine(SceneTransition.LoadScene("Story"));
		}

		public void freePlay() {
			StartCoroutine(SceneTransition.LoadScene("SongSelect"));
		}

		public void configure() {
			StartCoroutine(SceneTransition.LoadScene("Configure"));
		}

		public void quit() {
			Application.Quit();
		}

	}
}