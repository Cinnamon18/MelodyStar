using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePressEffect : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
		Destroy(gameObject, 0.5f);
	}

	// Update is called once per frame
	void Update() {

	}
}
