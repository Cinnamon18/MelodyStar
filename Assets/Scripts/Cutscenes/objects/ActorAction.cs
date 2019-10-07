using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene
{

	/*
	 * Actor does something. For example, entering and exiting the scene
	 */
	public class ActorAction : CutsceneObject
	{
		public string ActorName { get; }
		public string Action { get; }


		public ActorAction(string actorName, string action)
		{
			ActorName = actorName;
			Action = action;
		}

		public override void Log()
		{
			Debug.Log("Actor Action: (Cutscene Object)");
			Debug.Log("\tName of actor: " + ActorName);
			Debug.Log("\tAction: " + Action);
		}
	}
}
