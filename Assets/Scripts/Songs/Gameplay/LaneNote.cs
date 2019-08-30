using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Songs.Gameplay {
	public class LaneNote : MonoBehaviour {
		const float fallSpeed = 6; //Should be units per second, where the screen is 10 units tall
		const float rotateSpeed = 50;

		void Start() {
		}

		void Update() {
			this.transform.position += new Vector3(0, -fallSpeed) * Time.deltaTime;
			this.transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
		}

		public void createNote() {
		}
	}
}