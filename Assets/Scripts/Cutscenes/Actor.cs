using System.Collections;
using System.Collections.Generic;
using Cutscene.Elements;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using static Utilities.NamedPrefab;

namespace Cutscene {
	public class Actor : MonoBehaviour {
		public NamedSpriteStruct[] expressionNamedSprites;
		private Dictionary<string, Sprite> expressionSprites;
		public string actorName;
		public Image image;

		[HideInInspector]
		public CutscenePosition position;

		public void Awake() {
			expressionSprites = NamedPrefab.dictFromNamedSprites(expressionNamedSprites);
			image.sprite = expressionSprites["Neutral"];
		}

		public void changePose(string pose) {
			image.sprite = expressionSprites[pose];
		}

		public IEnumerator fadeIn() {
			yield return Util.fadeIn(image, 0.5f);
		}

		public IEnumerator fadeOut() {
			yield return Util.fadeOut(image, 0.5f);
		}

		public override string ToString() {
			return "Actor " + actorName + " in position " + position;
		}


	}
}