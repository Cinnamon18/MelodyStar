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

		public override IEnumerator doAction(CutsceneManager cutsceneManager) {

			Actor actor = cutsceneManager.actors[actorName];
			yield return actor.fadeOut();
			yield return cutsceneManager.dialogManager.removeActor(actor);
			cutsceneManager.actors.Remove(actorName);
			GameObject.Destroy(actor);
		}

		public override string ToString() {
			return "Actor Exit: (Cutscene Object)" + "\tName of actor: " + actorName;
		}
	}
}
