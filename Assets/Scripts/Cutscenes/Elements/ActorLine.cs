using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene.Elements {

	/*
	 * A piece of dialogue
	 */
	public class ActorLine : CutsceneElement {
		public string ActorName { get; }
		public string Pose { get; } //Image?
		public string Line { get; }


		public ActorLine(string actorName, string pose, string line) {
			ActorName = actorName;
			Pose = pose;	
			Line = line;
		}

		public ActorLine(string actorName, string line)
			: this(actorName, "neutral", line) {

		}

		public override string ToString() {
			return "Actor Line: (Cutscene Object)" + "\n\tName of actor: " + ActorName + "\\n\tPose: " + Pose + "\n\tLine: " + Line;
		}
	}
}

