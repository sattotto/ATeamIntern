using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	Vector3 offset;
	Vector3 target;
	float deg;

	float speed;
	float radius;

	Rigidbody _rigidbody = null;

	public Rigidbody RigidBody {
		get { return _rigidbody ?? (_rigidbody = gameObject.GetComponent<Rigidbody> ()); }
	}

	public void ballType(int type, float charge, float playerDeg){
		if (type == 0 || type == 4){
			// o型
			circleSet(2f*charge,1f,playerDeg);
		} else {
            Create(playerDeg, 5f*charge);
		}
	}

	/// <summary>
	/// i型のぼーる発射
	/// </summary>
	public void Create(float direction, float speed) {
		Vector3 v;
		v.x = Mathf.Cos (Mathf.Deg2Rad * direction) * speed;
		v.y = Mathf.Sin (Mathf.Deg2Rad * direction) * speed;
		v.z = 0;
		RigidBody.velocity = v;
		Destroy (this.gameObject, 0.25f);
	}

    public void ChangeSprite(Sprite sprite)
    {
        SpriteRenderer ballSprict = this.GetComponent<SpriteRenderer>();

        ballSprict.sprite = sprite;

    }

	/// <summary>
	/// m型のボール発射
	/// </summary>
	public void setoffset(Vector3 playerpos){
		offset = playerpos;
		SetTarget ( new Vector3(3,0,0), 60 );
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
		//this.offset = Player.playerPos;
		this.target = target;
		this.deg = deg;

		StartCoroutine ("ThrowBall");
	}

	/// <summary>
	/// Playerのぼーる投擲の種類
	/// </summary>
	/// <param name="speed">回転するスピード</param>
	/// <param name="radius">回転の半径</param>
	/// <param name="playerDig">プレイヤーの向いている角度</param>
	public void circleSet (float speed, float radius, float playerDeg){
		this.speed = 1.5f;
		this.radius = radius;
		this.deg = playerDeg;
		Destroy (this.gameObject, 1f);
		StartCoroutine ("circleMove");
	}

	IEnumerator circleMove(){
		for (float t = 0;;t += 0.1f){
			//Debug.Log ("t : " + t + " speed : " + speed + " t x speed = " + t*speed + " cos= " + Mathf.Cos (t * speed) + " sin= " + Mathf.Sin (t * speed));

			float x = Mathf.Cos(t * speed + deg*Mathf.Deg2Rad) * radius;
			float y = Mathf.Sin(t * speed + deg*Mathf.Deg2Rad) * radius;
			float z = 0f;
			transform.position = new Vector3(x, y, z) + Player.playerPos;

			yield return null;
		}
	}
}
