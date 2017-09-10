using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	private Camera _mainCamera;

	public static int PLAYER_HP_MAX = Const.PLAYER_HP;
	public static int playerHP;

	public GameObject ballPrefab;

	public float fieldTop;
	public float fieldBottom;
	public float fieldLeft;
	public float fieldRight;

	int shootMax;
	bool isReload = false;

	Vector3 prevPos = new Vector3( 0, 0, 0);
	float prevRot;

	// Use this for initialization
	void Start () {
		// カメラオブジェクトを取得します
		GameObject obj = GameObject.Find ("Main Camera");
		_mainCamera = obj.GetComponent<Camera> ();

		// 座標値を出力
		Debug.Log (getScreenTopLeft ().x + ", " + getScreenTopLeft ().y);
		Debug.Log (getScreenBottomRight ().x + ", " + getScreenBottomRight ().y);

		PLAYER_HP_MAX = Const.PLAYER_HP;
		playerHP = PLAYER_HP_MAX;

		shootMax = Const.SHOOT_NUM;
	}

	// Update is called once per frame
	void Update () {
		if (!isReload) {
			moveKeyboard ();
		} else {
			Invoke("flgChange",1f);
		}
		if (Input.GetKeyDown (KeyCode.Space) && !isReload) {
			shoot ();
		}
		checkPlayerRotation ();
	}

	void moveKeyboard(){
		if (Input.GetKey (KeyCode.LeftArrow) && fieldLeft < transform.position.x) {
			transform.Translate (-Const.SPEED[GameSpeedButton.speedCount], 0, 0);
		}
		if (Input.GetKey (KeyCode.RightArrow) && fieldRight > transform.position.x) {
			transform.Translate ( Const.SPEED[GameSpeedButton.speedCount], 0, 0);
		}
		if (Input.GetKey (KeyCode.UpArrow) && fieldTop > transform.position.y) {
			transform.Translate ( 0, Const.SPEED[GameSpeedButton.speedCount], 0);
		}
		if (Input.GetKey (KeyCode.DownArrow) && fieldBottom < transform.position.y) {
			transform.Translate ( 0, -Const.SPEED[GameSpeedButton.speedCount], 0);
		}
	}

	private Vector3 getScreenTopLeft() {
		// 画面の左上を取得
		Vector3 topLeft = _mainCamera.ScreenToWorldPoint (Vector3.zero);
		// 上下反転させる
		topLeft.Scale(new Vector3(1f, -1f, 1f));
		return topLeft;
	}

	private Vector3 getScreenBottomRight() {
		// 画面の右下を取得
		Vector3 bottomRight = _mainCamera.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0.0f));
		// 上下反転させる
		bottomRight.Scale(new Vector3(1f, -1f, 1f));
		return bottomRight;
	}

	void shoot(){
		shootMax -= 1;
		if (shootMax == 0) {
			isReload = true;
		}
		Instantiate (ballPrefab, transform.position, Quaternion.identity);
	}

	public static void PlayerDamaged(int damage){
		setPlayerHP (playerHP - damage);
		if (playerHP <= 0) {
			// がめおべら
			Debug.Log ("you died!");
			GameManager.gameOver = true;
		}
	}

	public static void PlayerHealed(int heal) {
		int setHP = System.Math.Min (PLAYER_HP_MAX, playerHP + heal);
		setPlayerHP (setHP);
	}

	public static void setPlayerHP(int HP){
		playerHP = HP;
		Debug.Log (playerHP);
	}

	void checkPlayerRotation(){
		float x = this.transform.position.x - prevPos.x;
		float y = this.transform.position.y - prevPos.y;

		Vector2 vec = new Vector2 (x, y).normalized;

		float rot = Mathf.Atan2 (vec.y, vec.x) * 180 / Mathf.PI;

		if(rot > 180) rot-= 360;
		if(rot <-180) rot+= 360;

		if (prevRot != rot) {
			Debug.Log ("Angle = " + rot);
			prevRot = rot;
			prevPos = this.transform.position;
		}
	}

	void flgChange (){
		isReload = false;
		shootMax = 5;
		Debug.Log ("now reloarding!");
	}
}
