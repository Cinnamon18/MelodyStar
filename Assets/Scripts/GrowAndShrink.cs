using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GrowAndShrink : MonoBehaviour {

	public float scaler;
	public float delay;

	void Start() {
		StartCoroutine(Grow());
	}

	public IEnumerator Grow() {
		Vector3 initial = GetComponent<RectTransform>().transform.localScale;
		Vector3 bigger = GetComponent<RectTransform>().transform.localScale * scaler;
		yield return Util.Lerp(delay, (float progress) => {
			GetComponent<RectTransform>().transform.localScale = bigger * progress + initial * (1 - progress);
		});
		yield return Util.Lerp(delay, (float progress) => {
			GetComponent<RectTransform>().transform.localScale = bigger * (1 - progress) + initial * progress;
		});
		StartCoroutine(Grow());
	}
}
