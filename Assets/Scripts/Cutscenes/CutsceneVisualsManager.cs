
using System.Collections;
using Cutscene.Elements;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Cutscene {
	public class CutsceneVisualsManager : MonoBehaviour {
		public GameObject canvas;
		public Image background;

		public IEnumerator addActor(Actor actor, CutscenePosition position) {
			yield return null;
			//TODO
		}

		public IEnumerator removeActor(Actor actor) {
			yield return null;
		}

		public IEnumerator setBackground(Sprite newBackgrond, float time) {
			if (time == 0) {
				this.background.sprite = newBackgrond;
			} else {
				yield return Util.fadeOut(background, time);
				this.background.sprite = newBackgrond;
				yield return Util.fadeIn(background, time);
			}
		}

	}
}