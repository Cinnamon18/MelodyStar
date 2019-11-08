using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLoop {
	public class GameLoop {
		int currentScene = 0;
		List<GameScene> gameScenes;

		public GameLoop(List<GameScene> gameScenes) : this(0, gameScenes) { }

		public GameLoop(int currentScene, List<GameScene> gameScenes) {
			this.currentScene = currentScene;
			this.gameScenes = gameScenes;
		}

		public GameScene pop() {
			currentScene++;
			return gameScenes[currentScene - 1];
		} 
	}
}
