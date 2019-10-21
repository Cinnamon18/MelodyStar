using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Cutscene.Elements {

	/*
	 * A piece of dialogue
	 */
	public class ActorLine : CutsceneElement {
		public string actorName { get; }
		public string pose { get; } //Image?
		public string line { get; }


		public ActorLine(string actorName, string pose, string line) {
			this.actorName = actorName;
			this.pose = pose;	
			this.line = line;
		}

		public ActorLine(string actorName, string line)
			: this(actorName, "neutral", line) {

		}

		public override IEnumerator doAction(
			CutsceneVisualsManager cutsceneVisuals,
			DialogManager dialogManager,
			Dictionary<string, Actor> actors) {
			
			Actor actor = null;
			try {
				actor = actors[actorName];
			} catch (KeyNotFoundException e) {
				Debug.LogError("Error: actor \"" + actorName + "\" for line \"" + line + "\" not found in actors list. Actors list contents: ");
				Debug.LogError(String.Join(", ", actors.ToArray()));
				throw e;
			}
			actor.changePose(pose);
			yield return dialogManager.sayText(actor, line);
		}

		public override string ToString() {
			return "Actor Line: (Cutscene Object)" + "\tName of actor: " + actorName + "\tPose: " + pose + "\tLine: " + line;
		}
	}
}

