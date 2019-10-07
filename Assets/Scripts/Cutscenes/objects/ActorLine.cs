using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene
{

	/*
	 * A piece of dialogue
	 */
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

		//Don't know if we're using .png, .jpeg or whatever.
		public string getImageFileName(string extension)
		{
			return ActorName + Pose + extension;
		}

		public override void Log()
		{
			Debug.Log("Actor Line: (Cutscene Object)");
			Debug.Log("\tName of actor: " + ActorName);
			Debug.Log("\tPose: " + Pose);
			Debug.Log("\tLine: " + Line);
	}
}

