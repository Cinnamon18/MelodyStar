using System;
using System.Collections;
using UnityEngine;

namespace Utilities {

	public class Util {

		// Lerping function. Straight from h*ng*****g, which was straight from from P*** t* H******, which was straight from Fl**red.
		public static IEnumerator Lerp(float duration, Action<float> perStep) {
			float timer = 0;
			while ((timer += Time.deltaTime) < duration) {
				perStep(timer / duration);
				yield return null;
			}
			perStep(1);
		}

	}

}
