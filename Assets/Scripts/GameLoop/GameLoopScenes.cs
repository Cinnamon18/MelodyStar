using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLoop {
	public class GameLoopScenes {
		int currentScene = 0;
		List<GameScene> gameScenes;

		public GameLoopScenes(List<GameScene> gameScenes) : this(0, gameScenes) { }

		public GameLoopScenes(int currentScene, List<GameScene> gameScenes) {
			this.currentScene = currentScene;
			this.gameScenes = gameScenes;
		}

		public GameScene pop() {
			currentScene++;
			return gameScenes[currentScene - 1];
		} 
	}
}
