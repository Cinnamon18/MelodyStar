using System.Collections;
using System.Collections.Generic;
using Songs.Gameplay;
using UnityEngine;

namespace GameLoop {
	public class SongScene : GameScene {

		public SongScene(string band, string sceneID) : base(band, sceneID) { }

		public override void advance() {
			SongManager cutsceneManager = Object.FindObjectOfType<SongManager>();
			cutsceneManager.bandName = band;
			cutsceneManager.songName = sceneID;
		}

		public override string getSceneType() { return "SongScoreTest"; }
	}
}
