using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour {

	Slider _slider;
	void Start () {
		// スライダーを取得する
		_slider = GameObject.Find("Slider").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		// HPゲージに値を設定
		_slider.value = Player.playerHP;
	}
}
