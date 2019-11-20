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
		print("uwu");
		yield return new WaitForSeconds(delay);
		GetComponent<RectTransform>().position = GetComponent<RectTransform>().position + new Vector3(0, height, 0);
		yield return new WaitForSeconds(delay);
		GetComponent<RectTransform>().position = GetComponent<RectTransform>().position - new Vector3(0, height, 0);
		StartCoroutine(Bounce());
	}
}
