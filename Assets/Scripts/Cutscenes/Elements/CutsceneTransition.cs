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
			Canvas canvas,
			DialogManager dialogManager,
			Dictionary<string, Actor> actors) {

			Actor actor = actors[actorName];
			yield return dialogManager.sayText(actor, Line);
		}

		public override string ToString() {
			return "Scene Transition: (Cutscene Object)" + "\n\tScene Name: " + scenePrefab;
		}
	}
}
