using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceUpAndDown : MonoBehaviour {

	public float height;
	public float delay;

	void Start() {
		StartCoroutine(Bounce());
	}

	public IEnumerator Bounce() {
		yield return new WaitForSeconds(delay);
		GetComponent<RectTransform>().transform.localPosition = GetComponent<RectTransform>().transform.localPosition + new Vector3(0, height, 0);
		yield return new WaitForSeconds(delay);
		GetComponent<RectTransform>().transform.localPosition = GetComponent<RectTransform>().transform.localPosition - new Vector3(0, height, 0);
		StartCoroutine(Bounce());
	}
}
