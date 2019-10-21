using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene.Elements {

	/*
	 * Actor does something. For example, entering and exiting the scene
	 */
	public class ActorExit : CutsceneElement {
		public string actorName { get; }

		public ActorExit(string actorName) {
			this.actorName = actorName;
		}

		public override IEnumerator doAction(
			CutsceneVisualsManager cutsceneVisuals,
			DialogManager dialogManager,
			Dictionary<string, Actor> actors) {

			Actor actor = actors[actorName];
			yield return actor.fadeOut();
			yield return dialogManager.removeActor(actor);
			actors.Remove(actorName);
			GameObject.Destroy(actor);
		}

		public override string ToString() {
			return "Actor Exit: (Cutscene Object)" + "\tName of actor: " + actorName;
		}
	}
}
