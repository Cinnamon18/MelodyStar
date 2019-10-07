using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Cutscene.Elements {

	/*
	 * A piece of dialogue
	 */
	public class ActorLine : CutsceneElement {
		public string actorName { get; }
		public string Pose { get; } //Image?
		public string Line { get; }


		public ActorLine(string actorName, string pose, string line) {
			this.actorName = actorName;
			Pose = pose;	
			Line = line;
		}

		public ActorLine(string actorName, string line)
			: this(actorName, "neutral", line) {

		}

		public override IEnumerator doAction(
			Canvas canvas,
			DialogManager dialogManager,
			Dictionary<string, Actor> actors) {
				
			Actor actor = actors[actorName];
			yield return dialogManager.sayText(actor, Line);
		}

		public override string ToString() {
			return "Actor Line: (Cutscene Object)" + "\n\tName of actor: " + actorName + "\\n\tPose: " + Pose + "\n\tLine: " + Line;
		}
	}
}

