using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cutscene.Elements;
using GameLoop;

namespace Cutscene {
	public class CutsceneManager : MonoBehaviour {
		public CutsceneParser cutsceneParser;
		public string bandName;
		public string cutsceneName;

		public CutsceneVisualsManager cutsceneVisuals;
		public DialogManager dialogManager;
		public Dictionary<string, Actor> actors = new Dictionary<string, Actor>();

		void Start() {
			TextAsset writing = Resources.Load<TextAsset>("Writing/" + bandName + "/" + cutsceneName);
			List<CutsceneElement> elements = cutsceneParser.parse(writing.text);

			// foreach (CutsceneElement elem in elements) {
			// 	Debug.Log(elem);
			// }
			StartCoroutine(playCutscene(elements));
		}

		private IEnumerator playCutscene(List<CutsceneElement> elements) {
			foreach(CutsceneElement element in elements) {
				yield return element.doAction(this);
			}

			yield return null;
			cutsceneDone();
		}

		// hehe hacks
		public void startCoroutine(IEnumerator coroutine) {
			StartCoroutine(coroutine);
		}
		
		private void cutsceneDone() {
			StartCoroutine(	Object.FindObjectOfType<GameLoopManager>().advance());
		}
	}
}

