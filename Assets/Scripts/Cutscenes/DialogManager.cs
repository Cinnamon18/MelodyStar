
using System.Collections;
using Cutscene.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Cutscene {
	public class DialogManager : MonoBehaviour {
		public Text dialog;
		public Text actorNameLabel;

		public IEnumerator sayText(Actor actor, string text) {
			yield return sayText(actor.actorName, text);
		}

		public IEnumerator sayText(string actorName, string text) {
			actorNameLabel.text = actorName;
			dialog.text = text;
			float startTime = Time.time;
			float duration = 1 + text.Length * 0.05f;
			yield return new WaitForSeconds(0.25f);
			while(Time.time - startTime < duration) {
				if(Input.anyKey) {
					break;
				}
				yield return null;
			}
		}

		public IEnumerator addActor(Actor actor) {
			// actorNameLabel.text = actor.actorName;
			yield return null;
		}

		public IEnumerator removeActor(Actor actor) {
			// actorNameLabel.text = "";
			yield return null;
		}
	}
}