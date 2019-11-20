using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLoop {
	public class GameLoopManager : MonoBehaviour {
		public static GameLoopScenes gameLoop;
		public SceneTransition sceneTransition;

		void Start() {
			if (GameLoopManager.gameLoop == null) {
				GameLoopManager.gameLoop = GameLoopManager.defaultGameLoop;
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

		public static readonly GameLoopScenes defaultGameLoop = new GameLoopScenes(
					new List<GameScene>() {
						new CutsceneScene("Tutorial", "IntroScene"),
						new SongScene("Tutorial", "tutorial_song_alice_moh"),
						new CutsceneScene("Metal", "IntroScene"),
						new SongScene("Metal", "story_by_brad_baker"),
						new CutsceneScene("Frog", "IntroScene"),
						new SongScene("Frog", "doug_ground_up_for_meat"),
						new CutsceneScene("Chiptune", "IntroScene"),
						new SongScene("Chiptune", "battle_mode"),
						// new CutsceneScene("JPop", "IntroScene"),
						new CutsceneScene("Frog", "Scene2"),
						new SongScene("Frog", "chrissy_by_megupets"),
						new CutsceneScene("Chiptune", "Scene2"),
						new SongScene("Chiptune", "accion_by_djsaryon"),
						new SongScene("Metal", "body_count_by_t4ngr4m"),
						new SongScene("Metal", "cargo_by_fredrik_johansson"),
						new SongScene("Metal", "tokyo_by_t4ng4m"),
						new SongScene("JPop", "at_the_lake_by_t4ngr4m"),
						new SongScene("JPop", "jpop_punk_by_t4ngr4m"),
						new SongScene("JPop", "just_a_day_by_t4ngr4m"),
						new SongScene("JPop", "melon_by_frederik"),
						new SongScene("JPop", "solitude_by_t4ngr4m"),
						new SongScene("JPop", "wind_in_rose_by_t4ngr4m"),
						new SongScene("Frog", "mushrooms_by_t4ngr4m"),
						new SongScene("Chiptune", "archville_by_frederik"),
						new SongScene("Chiptune", "legend_by_frederik"),
					}
				);
	}
}
