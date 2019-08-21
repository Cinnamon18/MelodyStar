using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class SceneTransition : MonoBehaviour {

	private static GameObject sceneTransitionCanvas;
	private static AnimationCurve currentTransCurve;
	public AnimationCurve animCurve;

	private static readonly float TRANSITION_TIME = 2f;

	void Awake() {
		sceneTransitionCanvas = Resources.Load<GameObject>("SceneTransitionCanvas");
		if (currentTransCurve == null) {
			SceneTransition.currentTransCurve = animCurve;
		}

		StartCoroutine(FadeIn());
	}

	public static IEnumerator LoadScene(string scene) {
		if (sceneTransitionCanvas == null) {
			throw new UnityException("Please make sure you've assigned a scenetransitioncanvas to the scene transition mb at some point.");
		}

		GameObject sceneTransCan = Object.Instantiate(sceneTransitionCanvas);
		RectTransform mask = sceneTransCan.transform.GetChild(0).gameObject.GetComponent<RectTransform>();

		yield return SceneTransition.Lerp(SceneTransition.TRANSITION_TIME, progress => {
			float scaler = 1 - currentTransCurve.Evaluate(progress);
			mask.sizeDelta = new Vector2(scaler * 1600, scaler * 900);
		});

		yield return new WaitForSeconds(0.1f);
		SceneManager.LoadScene(scene);
	}

	private static IEnumerator FadeIn() {
			GameObject sceneTransCan = Object.Instantiate(sceneTransitionCanvas);
			RectTransform mask = sceneTransCan.transform.GetChild(0).gameObject.GetComponent<RectTransform>();

			yield return SceneTransition.Lerp(SceneTransition.TRANSITION_TIME, progress => {
				float scaler = currentTransCurve.Evaluate(progress);
				mask.sizeDelta = new Vector2(scaler * 1600, scaler * 900);
			});

			Object.Destroy(sceneTransCan);
	}

	private static IEnumerator Lerp(float duration, Action<float> perStep) {
		float timer = 0;
		while ((timer += Time.unscaledDeltaTime) < duration) {
			perStep(timer / duration);
			yield return null;
		}
		perStep(1);
	}
}
