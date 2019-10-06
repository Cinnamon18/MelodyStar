using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene
{

	public class ActorLine : CutsceneObject
	{
		public string ActorName { get; }
		public string Pose { get; }	//Image?
		public string Line { get; }
		

		public ActorLine(string actorName, string pose, string line)
		{
			ActorName = actorName;
			Pose = pose;
			Line = line;
		}

		public ActorLine(string actorName, string line)
			: this(actorName, "neutral", line)
		{

		}

		public override void Log()
		{
			Debug.Log("Name of actor: " + ActorName);
			Debug.Log("Pose: " + Pose);
			Debug.Log("Line: \n" + Line);
		}
	}
}

