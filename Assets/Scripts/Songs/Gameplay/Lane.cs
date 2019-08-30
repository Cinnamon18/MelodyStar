using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Songs.Gameplay {
	public class Lane : MonoBehaviour {

		public GameObject notePrefab;

		public void createNote() {
			GameObject note = Instantiate(notePrefab, this.transform.position, notePrefab.transform.rotation);
			note.GetComponent<SpriteRenderer>().size = new Vector2(
				gameObject.GetComponent<SpriteRenderer>().size.x * 0.9f,
				gameObject.GetComponent<SpriteRenderer>().size.x * 0.9f
			);

		}
	}
}