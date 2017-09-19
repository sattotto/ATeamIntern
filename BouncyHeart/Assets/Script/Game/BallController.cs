using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	Vector3 offset;
	Vector3 target;
	float deg;

	Rigidbody _rigidbody = null;

	public Rigidbody RigidBody {
		get { return _rigidbody ?? (_rigidbody = gameObject.GetComponent<Rigidbody> ()); }
	}

	public void setoffset(Vector3 playerpos){
		offset = playerpos;
//		Debug.Log (offset);
		SetTarget ( new Vector3(3,0,0), 60 );
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

	IEnumerator ThrowBall()
	{
		float b = Mathf.Tan (deg * Mathf.Deg2Rad);
		float a = (target.y - b * target.x) / (target.x * target.x);

		for (float x = 0; x <= this.target.x; x+= 0.125f)
		{
			float y = a * x * x + b * x;
			transform.position = new Vector3 (x, y, 0) + offset;
			yield return null;
		}
	}

	public void SetTarget(Vector3 target, float deg)
	{
//		this.offset = Player.playerPos;
		this.target = target;
		this.deg = deg;

		StartCoroutine ("ThrowBall");
	}

}
