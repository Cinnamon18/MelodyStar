using System.Collections;
using System.Collections.Generic;
using Songs.Model;
using UnityEngine;

namespace Songs.Gameplay {
	public class Lane : MonoBehaviour {

		public GameObject notePrefab, noteTargetPrefab, pressVFxPrefab;
		public GameObject perfectEffect, goodEffect, missEffect;
		[HideInInspector]
		public GameObject noteTarget;
		private float width;
		private Queue<GameObject> notes = new Queue<GameObject>();


		void Start() {
			width = gameObject.GetComponent<SpriteRenderer>().size.x;

			noteTarget = Instantiate(noteTargetPrefab, this.transform.position + new Vector3(0, -9), noteTargetPrefab.transform.rotation);
			noteTarget.GetComponent<SpriteRenderer>().size = new Vector2(width * 0.9f, noteTarget.GetComponent<SpriteRenderer>().size.y);
		}

		public void createNote() {
			GameObject note = Instantiate(notePrefab, this.transform.position, notePrefab.transform.rotation);
			note.GetComponent<SpriteRenderer>().size = new Vector2(width * 0.9f, width * 0.9f);
			notes.Enqueue(note);
		}

		public GameObject getLowestNote() {
			if (notes.Count > 0) {
				GameObject note = notes.Dequeue();
				return note;
			} else {
				return null;
			}
		}

		public void makePressVFx() {
			Destroy(Instantiate(pressVFxPrefab, noteTarget.transform.position, pressVFxPrefab.transform.rotation), 4);
		}

		public void noteTapVFx(PressAccuracy accuracy) {
			switch (accuracy) {
				case PressAccuracy.Perfect:
					Instantiate(perfectEffect, noteTarget.transform.position, perfectEffect.transform.rotation);
					break;
				case PressAccuracy.Good:
					Instantiate(goodEffect, noteTarget.transform.position, goodEffect.transform.rotation);
					break;
				case PressAccuracy.Miss:
					Instantiate(missEffect, noteTarget.transform.position, missEffect.transform.rotation);
					break;

			}
		}
	}
}