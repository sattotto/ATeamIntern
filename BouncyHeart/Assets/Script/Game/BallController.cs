using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	void Update () {
		transform.Translate (0, 0.2f, 0);

		if (transform.position.y > 5) {
			Destroy (gameObject);
		}
	}
}
