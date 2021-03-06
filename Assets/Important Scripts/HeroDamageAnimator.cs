﻿using UnityEngine;
using System.Collections;

public class HeroDamageAnimator : MonoBehaviour {
	float timerLimitor = 0.7f;
	float timer = 0.0f;

	int counter = 0;

	int heroDamage;

	void Start(){
		heroDamage = GameObject.FindGameObjectWithTag("BattleScript").GetComponent<BattleScript>().HeroDamage;
	}

	void Update(){
		timer += Time.deltaTime;
		--counter;

		if(timer >= timerLimitor)
			Destroy(gameObject);
	}

	void OnGUI(){
		GUI.Label(new Rect((Screen.width / 2), Screen.height - 100 + counter, 100, 100), "<color=red><size=40>" + "-" + heroDamage.ToString() + "</size></color>");
	}
}
