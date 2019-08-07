using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menuing {
	public class MainMenu : MonoBehaviour {
		public void freePlay() {
			SceneManager.LoadScene("SongSelect");
		}

		public void setupMidi() {
			SceneManager.LoadScene("Configure");
		}

	}
}