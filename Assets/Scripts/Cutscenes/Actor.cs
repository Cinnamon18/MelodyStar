using System.Collections;
using Cutscene.Elements;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Cutscene {
	public class Actor : MonoBehaviour {
		public Image image;
		public string name;
		public CutscenePosition position;

		public IEnumerator fadeIn() {
			yield return Util.Lerp(0.5f, (float progress) => {
				image.color = new Color(image.color.r, image.color.g, image.color.b, progress);
			});
		}

		public IEnumerator fadeOut() {
			yield return Util.Lerp(0.5f, (float progress) => {
				image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - progress);
			});
		}

	}
}