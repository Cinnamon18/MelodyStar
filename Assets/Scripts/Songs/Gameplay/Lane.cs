using System.Collections;
using System.Collections.Generic;
using Songs.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Songs.Gameplay {
	public class Lane : MonoBehaviour {

		public Text notename;
		public GameObject notePrefab, noteTargetPrefab, pressVFxPrefab;
		public GameObject perfectEffect, goodEffect, missEffect;
		[HideInInspector]
		public GameObject noteTarget;
		private float width;
		private Queue<LaneNote> notes = new Queue<LaneNote>();

		void Start() {
			width = gameObject.GetComponent<SpriteRenderer>().size.x;

			noteTarget = Instantiate(noteTargetPrefab, this.transform.position + new Vector3(0, -9), noteTargetPrefab.transform.rotation);
			noteTarget.GetComponent<SpriteRenderer>().size = new Vector2(width * 0.9f, noteTarget.GetComponent<SpriteRenderer>().size.y);
		}

		public void createNote(SongNote songNote) {
			GameObject noteGO = Instantiate(notePrefab, this.transform.position, notePrefab.transform.rotation);
			LaneNote laneNote = noteGO.GetComponent<LaneNote>();
			laneNote.setSpriteSizes(new Vector2(width * 0.9f, width * 0.9f));
			laneNote.setNoteWidth(songNote.endTime - songNote.startTime);
			notes.Enqueue(laneNote);
		}

		public LaneNote getLowestNote() {
			if (notes.Count > 0) {
				LaneNote note = notes.Peek();
				return note;
			} else {
				return null;
			}
		}

		public void destroyLowestNote() {
			if (notes.Count > 0) {
				Destroy(notes.Dequeue().gameObject);
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