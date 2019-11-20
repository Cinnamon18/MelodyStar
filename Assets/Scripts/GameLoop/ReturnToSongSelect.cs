using System.Collections;
using System.Collections.Generic;
using Songs.Gameplay;
using UnityEngine;

namespace GameLoop {
	public class ReturnToSongSelect : GameScene {

		public ReturnToSongSelect() : base("", "") { }

		public override void advance() {

		}

		public override string getSceneType() { return "SongSelect"; }
	}
}
