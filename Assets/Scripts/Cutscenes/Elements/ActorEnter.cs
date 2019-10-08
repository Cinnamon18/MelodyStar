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
		public string actorName { get; }

		public ActorEnter(string actorName, GameObject actorPrefab, CutscenePosition position) {
			this.actorName = actorName;
			this.actorPrefab = actorPrefab;
			this.position = position;
		}

		public override IEnumerator doAction(
			Canvas canvas,
			DialogManager dialogManager,
			Dictionary<string, Actor> actors) {

			GameObject actorGO = Object.Instantiate(actorPrefab, canvas.transform);
			Actor actor = actorGO.GetComponent<Actor>();
			actors.Add(actorName, actor);
			yield return null;
		}

		public override string ToString() {
			return "Actor Enter: (Cutscene Object)" + "\n\tName of actor: " + actorName + " \n\t Side of actor: " + position;
		}
	}
}
