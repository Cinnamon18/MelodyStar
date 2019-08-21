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

	private static readonly float TRANSITION_TIME = 0.3f;

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
		Image image = sceneTransCan.GetComponentInChildren<Image>();

		yield return SceneTransition.Lerp(SceneTransition.TRANSITION_TIME, progress => {
			float scaler = currentTransCurve.Evaluate(progress);
			Color opacityChangedColor = image.color;
			opacityChangedColor.a = scaler;
			image.color = opacityChangedColor;
		});

		yield return new WaitForSeconds(0.1f);
		SceneManager.LoadScene(scene);
	}

	private static IEnumerator FadeIn() {
		GameObject sceneTransCan = Object.Instantiate(sceneTransitionCanvas);
		Image image = sceneTransCan.GetComponentInChildren<Image>();

		yield return SceneTransition.Lerp(SceneTransition.TRANSITION_TIME, progress => {
			float scaler = 1 - currentTransCurve.Evaluate(progress);
			Color opacityChangedColor = image.color;
			opacityChangedColor.a = scaler;
			image.color = opacityChangedColor;
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
