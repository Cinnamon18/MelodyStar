using System.Collections;
using System.Collections.Generic;
using Cutscene.Elements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cutscene {
	public class DialogManager : MonoBehaviour {
		public Label textField;

		public IEnumerator sayText(Actor actor, string text) {
			textField.text = text;
			yield return new WaitForSeconds(1 + text.Length * 0.05f);
		}
	}
}