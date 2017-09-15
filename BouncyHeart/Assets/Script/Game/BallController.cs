using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	Rigidbody _rigidbody = null;

	public Rigidbody RigidBody {
		get { return _rigidbody ?? (_rigidbody = gameObject.GetComponent<Rigidbody> ()); }
	}

	public void Create(float direction, float speed) {
		Vector3 v;
		v.x = Mathf.Cos (Mathf.Deg2Rad * direction) * speed;
		v.y = Mathf.Sin (Mathf.Deg2Rad * direction) * speed;
		v.z = 0;
		RigidBody.velocity = v;
		Destroy (this.gameObject, 1f);
	}

    public void ChangeSprite(Sprite sprite)
    {
        SpriteRenderer ballSprict = this.GetComponent<SpriteRenderer>();

        ballSprict.sprite = sprite;

    }
}
