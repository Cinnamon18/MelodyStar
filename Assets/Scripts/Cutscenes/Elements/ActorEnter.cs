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

		public override string ToString() {
			return "Actor Enter: (Cutscene Object)" + "\n\tName of actor: " + actorName + " \n\t Side of actor: " + position;
		}
	}
}
