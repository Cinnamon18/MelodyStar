using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cutscene.Elements;

namespace Cutscene {
	public class CutsceneManager : MonoBehaviour {
		public CutsceneParser cutsceneParser;
		public TextAsset writing;

		public Canvas canvas;
		public DialogManager dialogManager;
		public Dictionary<string, Actor> actors = new Dictionary<string, Actor>();

		void Start() {
			List<CutsceneElement> elements = cutsceneParser.parse(writing.text);

			foreach (CutsceneElement elem in elements) {
				Debug.Log(elem);
			}
			StartCoroutine(playCutscene(elements));
		}

		private IEnumerator playCutscene(List<CutsceneElement> elements) {
			foreach(CutsceneElement element in elements) {
				element.doAction(canvas, dialogManager, actors);
			}

			yield return null;
			cutsceneDone();
		}
		
		private void cutsceneDone() {
			//TODO what do we do when we're done? idk!
		}
	}
}

