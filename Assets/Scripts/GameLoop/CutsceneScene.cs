using System.Collections;
using System.Collections.Generic;
using Cutscene;
using UnityEngine;

namespace GameLoop {
	public class CutsceneScene : GameScene {

		public CutsceneScene(string band, string sceneID) : base(band, sceneID) { }

		public override void advance() {
			CutsceneManager cutsceneManager = Object.FindObjectOfType<CutsceneManager>();
			Debug.Log("uwuuww");
			Debug.Log(band);
			Debug.Log(sceneID);
			cutsceneManager.bandName = band;
			cutsceneManager.cutsceneName = sceneID;
		}

		public override string getSceneType() { return "Story"; }
	}
}
