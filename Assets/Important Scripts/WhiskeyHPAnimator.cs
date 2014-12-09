using UnityEngine;
using System.Collections;

public class WhiskeyHPAnimator : MonoBehaviour {
	float timerLimitor = 0.4f;
	float timer = 0.0f;

	int counter = 0;

	public int healthPoint;

	void Update(){
		timer += Time.deltaTime;
		--counter;

		if(timer >= timerLimitor){
			GameObject.Find("Battle").GetComponent<BattleScript>().timerOn = true;
			Destroy(gameObject);
		}
	}

	void OnGUI(){
		GUI.Label(new Rect((Screen.width / 2), Screen.height - 100 + counter, 100, 100), "<color=green><size=40>" + "+" + healthPoint.ToString() + "</size></color>");
	}
}
