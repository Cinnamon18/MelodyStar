using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLoop {
	public abstract class GameScene {

		public string band;
		public string sceneID;

		public GameScene(string band, string sceneID) {
			this.band = band;
			this.sceneID = sceneID;
		}

		public abstract void advance();
		public abstract string getSceneType();
	}
}
