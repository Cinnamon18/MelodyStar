
using System.Collections;
using Cutscene.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Cutscene {
	public class DialogManager : MonoBehaviour {
		public Text dialog;
		public Text leftName;
		public Text rightName;

		public IEnumerator sayText(Actor actor, string text) {
			dialog.text = text;
			yield return new WaitForSeconds(1 + text.Length * 0.05f);
		}

		public IEnumerator addActor(Actor actor) {
			if(actor.position == CutscenePosition.Left || actor.position == CutscenePosition.Center) {
				leftName.text = actor.actorName;
			} else if(actor.position == CutscenePosition.Right) {
				rightName.text = actor.actorName;
			}
			yield return null;
		}

		public IEnumerator removeActor(Actor actor) {
			if(actor.position == CutscenePosition.Left || actor.position == CutscenePosition.Center) {
				leftName.text = "";
			} else if(actor.position == CutscenePosition.Right) {
				rightName.text = "";
			}
			yield return null;
		}
	}
}