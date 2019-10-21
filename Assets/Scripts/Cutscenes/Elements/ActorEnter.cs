using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene.Elements {

	/*
	 * Actor does something. For example, entering and exiting the scene
	 */
	public class ActorEnter : CutsceneElement {
		public GameObject actorPrefab { get; }
		public CutscenePosition position { get; }
		private string actorName;

		public ActorEnter(string actorName, GameObject actorPrefab, CutscenePosition position) {
			this.actorPrefab = actorPrefab;
			this.position = position;
			this.actorName = actorName;
		}

		public override IEnumerator doAction(
			CutsceneVisualsManager cutsceneVisuals,
			DialogManager dialogManager,
			Dictionary<string, Actor> actors) {

			GameObject actorGO = Object.Instantiate(actorPrefab, cutsceneVisuals.canvas.transform);

			Actor actor = actorGO.GetComponent<Actor>();
			actor.position = position;
			actors.Add(actor.actorName, actor);

			yield return cutsceneVisuals.addActor(actor, position);
			yield return dialogManager.addActor(actor);
			yield return actor.fadeIn();
		}

		public override string ToString() {
			return "Actor Enter: (Cutscene Object)" + "\tName of actor: " + actorName + " \t Side of actor: " + position;
		}
	}
}
