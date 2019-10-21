using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

		public static IEnumerator fadeIn(Image image) {
			yield return Util.Lerp(0.5f, (float progress) => {
				image.color = new Color(image.color.r, image.color.g, image.color.b, progress);
			});
		}

		public static IEnumerator fadeOut(Image image) {
			yield return Util.Lerp(0.5f, (float progress) => {
				image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - progress);
			});
		}
	}


	// Use like this: 
	// public NamedPrefabStruct<GameObject>[] namedPrefabs;
	// private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
	// void Awake() {
	// 	prefabs = NamedPrefab.dictFromNamedPrefabs(namedPrefabs);
	// }

	// TODO HAHA FIGURE OUT A WAY TO MAKE UNITY PLAY NICE W GENERICS

	public class NamedPrefab {
		[Serializable]
		public struct NamedPrefabStruct {
			public string name;
			public GameObject prefab;
		}

		[Serializable]
		public struct NamedSpriteStruct {
			public string name;
			public Sprite prefab;
		}

		public static Dictionary<string, GameObject> dictFromNamedPrefabs(IEnumerable<NamedPrefabStruct> namedPrefabs) {
			Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
			foreach (NamedPrefabStruct prefabEditor in namedPrefabs) {
				prefabs.Add(prefabEditor.name, prefabEditor.prefab);
			}
			return prefabs;
		}

		public static Dictionary<string, Sprite> dictFromNamedSprites(IEnumerable<NamedSpriteStruct> namedPrefabs) {
			Dictionary<string, Sprite> images = new Dictionary<string, Sprite>();
			foreach (NamedSpriteStruct prefabEditor in namedPrefabs) {
				images.Add(prefabEditor.name, prefabEditor.prefab);
			}
			return images;
		}
	}




}
