using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene.Elements {

	/*
	 * Transition to a new scene
	 */
	public class CutsceneTransition : CutsceneElement {
		public GameObject scenePrefab { get; }


		public CutsceneTransition(GameObject scenePrefab) {
			this.scenePrefab = scenePrefab;
		}

		public override IEnumerator doAction(
			CutsceneVisualsManager cutsceneVisuals,
			DialogManager dialogManager,
			Dictionary<string, Actor> actors) {

			
			
			yield return null;
		}

		public override string ToString() {
			return "Scene Transition: (Cutscene Object)" + "\tScene Name: " + scenePrefab;
		}
	}
}
