
using System.Collections;
using Cutscene.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Cutscene {
	public class CutsceneVisualsManager : MonoBehaviour {
		public GameObject canvas;

		public IEnumerator addActor(Actor actor, CutscenePosition position) {
			yield return null;
			//TODO
		}

		public IEnumerator removeActor(Actor actor) {
			yield return null;
		}
	}
}