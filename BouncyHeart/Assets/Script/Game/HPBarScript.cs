using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour {

	Slider _slider;
    private GameObject _parent;

	void Start () {
		// スライダーを取得する
		_slider = GameObject.Find("Slider").GetComponent<Slider>();
		_slider.maxValue = Const.PLAYER_HP;

		//親オブジェクトを取得
		_parent = transform.root.gameObject;

		Debug.Log("Parent:" + _parent.name);
	}
	
	// Update is called once per frame
	void Update () {
		// HPゲージに値を設定
		_slider.value = Player.playerHP;
	}
}
