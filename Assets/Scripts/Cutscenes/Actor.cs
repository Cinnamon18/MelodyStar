using System;
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
		public bool isDark = false;

		public void Awake() {
			expressionSprites = NamedPrefab.dictFromNamedSprites(expressionNamedSprites);
			image.sprite = expressionSprites["Neutral"];
		}

		public IEnumerator changePose(string pose) {
			yield return Util.fadeOut(image, 0.1f);
			try {
				image.sprite = expressionSprites[pose];
			} catch (Exception e) {
				Debug.LogError("Error with pose " + pose + " for actor " + this);
				throw e;
			}
			yield return Util.fadeIn(image, 0.1f);
		}

		public IEnumerator fadeIn() {
			yield return Util.fadeIn(image, 0.25f);
		}

		public IEnumerator fadeOut() {
			yield return Util.fadeOut(image, 0.25f);
		}

		public IEnumerator brighten() {
			isDark = false;
			yield return Util.Lerp(0.25f, (float progress) => {
				progress = 1f * progress;
				image.color = new Color(progress, progress, progress);
			});
		}

		public IEnumerator darken() {
			isDark = true;
			yield return Util.Lerp(0.25f, (float progress) => {
				progress = 0.7f * progress;
				image.color = new Color(1 - progress, 1 - progress, 1 - progress);
			});
		}

		public override string ToString() {
			return "Actor " + actorName + " in position " + position;
		}


	}
}