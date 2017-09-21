using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaegeBar : MonoBehaviour {

	public static Image chaegeBar;

	void Start(){
		initParameter();
	}
	
	void Update(){
		chargeGauge();
	}

	private void initParameter() {
        chaegeBar = GameObject.Find("ChargeBar").GetComponent<Image>();
        chaegeBar.fillAmount = 1;
    }

	public static void chargeGauge(){
		chaegeBar.fillAmount = Player.charge - 1f;
	}
}
