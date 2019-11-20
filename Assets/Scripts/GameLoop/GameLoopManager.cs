using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLoop {
	public class GameLoopManager : MonoBehaviour {
		public static GameLoop gameLoop;
		public SceneTransition sceneTransition;

		void Start() {
			if (GameLoopManager.gameLoop == null) {
				GameLoopManager.gameLoop = new GameLoop(
					new List<GameScene>() {
						new CutsceneScene("Test", "CenterTest"),
						new CutsceneScene("Test", "DemoShortScene1"),
						new CutsceneScene("Test", "DemoShortScene3"),
						new SongScene("Metal", "story_by_brad_baker"),
						new CutsceneScene("Test", "DemoShortScene2"),
						new SongScene("Chiptune", "battle_mode"),
					}
				);
			}

			SceneManager.sceneLoaded += doSceneSetup;
		}

		//Do it this uggo way bc coroutines die on load. which makes me sad.
		public GameScene nextScene;
		void doSceneSetup(Scene aScene, LoadSceneMode aMode) {
			if (nextScene != null) {
				nextScene.advance();
				nextScene = null;
			}
		}

		public IEnumerator advance() {
			nextScene = gameLoop.pop();
			yield return SceneTransition.LoadScene(nextScene.getSceneType());
		}
	}
}
