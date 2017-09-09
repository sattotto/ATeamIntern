using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	private Camera _mainCamera;

	public int PLAYER_HP_MAX = Const.PLAYER_HP;
	public int playerHP;

	public GameObject ballPrefab;

	public float fieldTop;
	public float fieldBottom;
	public float fieldLeft;
	public float fieldRight;

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

	}

	// Update is called once per frame
	void Update () {
		moveKeyboard ();
		if (Input.GetKeyDown (KeyCode.Space)) {
			shoot ();
		}
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
		Instantiate (ballPrefab, transform.position, Quaternion.identity);
	}
}
