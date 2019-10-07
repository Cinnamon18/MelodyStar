using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene
{

	/*
	 * Transition to a new scene
	 */
	public class SceneTransition : CutsceneObject
	{
		public string SceneName { get; }


		public SceneTransition(string sceneName)
		{
			SceneName = sceneName;
		}

		public override void Log()
		{
			Debug.Log("Scene Transition: (Cutscene Object)");
			Debug.Log("\tScene Name: " + SceneName);
		}
	}
}
