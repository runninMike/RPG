using UnityEngine;
using System.Collections;

public class HeroDamageAnimator : MonoBehaviour {
	float timerLimitor = 0.4f;
	float timer = 0.0f;

	int counter = 0;

	public int heroDamage;

	void Update(){
		timer += Time.deltaTime;
		--counter;

		if(timer >= timerLimitor)
			Destroy(gameObject);
	}

	void OnGUI(){
		GUI.Label(new Rect((Screen.width / 2), Screen.height + counter, 100, 100), "<color=red><size=40>" + "-" + heroDamage.ToString() + "</size></color>");
	}
}
