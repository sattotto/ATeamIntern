using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedButton : MonoBehaviour {
	
	public static int speedCount = 1;

	public void ButtonPush() {
		Debug.Log("Button Push !!");
//		speedCount += 1;
//		if (speedCount > Const.SPEED.Length - 1) {
//			speedCount = 0;
//		}
		// Textコンポーネント郡を取得します。
//		var components = this.gameObject.GetComponentsInChildren<Text>();
		// テキストを文字の状態によって変更するようにします。
//		components[0].text = "x"+(speedCount+1);
	}
}
