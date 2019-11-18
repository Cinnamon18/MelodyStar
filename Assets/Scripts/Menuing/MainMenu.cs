using System.Linq;
using CustomInput;
using UnityEngine;
using GameLoop;

namespace Menuing {
	public class MainMenu : MonoBehaviour {
    
		public GameLoopManager gameLoopManager;

		void Start() {
			if(PlayerPrefs.HasKey("saved"))
			{
				InputSettings.setToPrefs();
			} 
      else if (!InputSettings.initalized) 
      {
				InputSettings.setToDefault();
			}
		}

		public void story() {
			StartCoroutine(gameLoopManager.advance());
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