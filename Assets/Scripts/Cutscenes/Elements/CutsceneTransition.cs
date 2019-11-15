using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscene.Elements {

	/*
	 * Transition to a new scene
	 */
	public class CutsceneTransition : CutsceneElement {
		public Sprite background { get; }
		public bool isFirstBackground;


		public CutsceneTransition(Sprite background, bool isFirstBackground) {
			this.background = background;
			this.isFirstBackground = isFirstBackground;
		}

		public override IEnumerator doAction(CutsceneManager cutsceneManager) {


			yield return cutsceneManager.cutsceneVisuals.setBackground(background, isFirstBackground ? 0.0f : 0.5f);
		}

		public override string ToString() {
			return "Scene Transition: (Cutscene Object)" + "\tScene Name: " + background;
		}
	}
}
