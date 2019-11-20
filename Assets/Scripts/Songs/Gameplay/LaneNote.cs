using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Songs.Gameplay {
	public class LaneNote : MonoBehaviour {
		const float fallSpeed = 8; //Should be units per second, where the screen is 10 units tall
		const float rotateSpeed = 50;
		const float spriteHeight = 8; // Manually encode height of the sprite
		const float holdNoteCutoff = 0.200f; // Ms to qualify a note as a hold note (rather than a press once)

		public GameObject bottomCircle;
		public GameObject noteConnector;
		public GameObject topCircle;
		public bool isHoldNote = true;
		public bool isBeingPressed = false;

		void Start() {
		}

		void Update() {
			this.transform.position += new Vector3(0, -fallSpeed) * Time.deltaTime;
			// bottomCircle.transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
			if (topCircle != null) {
				// topCircle.transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
			}
		}

		public void setNoteWidth(float time) {
			if (time < holdNoteCutoff) {
				Destroy(topCircle);
				Destroy(noteConnector);
				isHoldNote = false;
			} else {
				Vector2 size = noteConnector.GetComponent<SpriteRenderer>().size;
				print(time);
				print(size);
				float stretchFactor = (time * fallSpeed);
				print(stretchFactor);
				noteConnector.GetComponent<SpriteRenderer>().size = new Vector2(size.x, stretchFactor);
				// topCircle.transform.position = noteConnector.transform.position + new Vector3(0, position, 0);
			}
		}

		public void setSpriteSizes(Vector2 size) {
			// this.transform.localScale = new Vector3(size.x, size.y, this.transform.localScale.z);
			bottomCircle.GetComponent<SpriteRenderer>().size = size;
			noteConnector.GetComponent<SpriteRenderer>().size = size;
			topCircle.GetComponent<SpriteRenderer>().size = size;
		}
	}
}