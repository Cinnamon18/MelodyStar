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
		public string pose { get; }
		public string line { get; }


		public ActorLine(string actorName, string pose, string line) {
			this.actorName = actorName;
			this.pose = pose;
			this.line = line;
		}

		public ActorLine(string actorName, string line)
			: this(actorName, "neutral", line) {

		}

		public override IEnumerator doAction(CutsceneManager cutsceneManager) {

			Actor actor = null;
			if (cutsceneManager.actors.ContainsKey(actorName)) {
				actor = cutsceneManager.actors[actorName];
				cutsceneManager.startCoroutine(actor.changePose(pose));
				if (actor.isDark) {
					Debug.Log("brightening " + actor);
					cutsceneManager.startCoroutine(actor.brighten());
				}
				List<Actor> actorList = cutsceneManager.actors.Values.ToList();
				foreach (Actor darkenActor in actorList) {
					// Debug.Log("darkening " + darkenActor);
					// Debug.Log(actor);
					// Debug.Log(!(actor == darkenActor));
					if ((!darkenActor.isDark) && (!(darkenActor == actor))) {
						cutsceneManager.startCoroutine(darkenActor.darken());
					}
				}
				yield return cutsceneManager.dialogManager.sayText(actor, line);
			} else {
				yield return cutsceneManager.dialogManager.sayText(actorName, line);
			}
		}

		public override string ToString() {
			return "Actor Line: (Cutscene Object)" + "\tName of actor: " + actorName + "\tPose: " + pose + "\tLine: " + line;
		}
	}
}

