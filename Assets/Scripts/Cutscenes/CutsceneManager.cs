using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cutscene.Elements;

namespace Cutscene {
	public class CutsceneManager : MonoBehaviour {
		public CutsceneParser cutsceneParser;
		public TextAsset writing;

		void Start() {
			List<CutsceneElement> objects = cutsceneParser.parse(writing.text);

			foreach (CutsceneElement obj in objects) {
				Debug.Log(obj);
			}
		}

		void Update() {

		}
	}
}

