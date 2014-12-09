using UnityEngine;
using System.Collections;

public class EnemyDamageAnimation : MonoBehaviour {
	public float x;
	public float y;

	float timerLimitor = 0.4f;
	float timer = 0.0f;

	int counter = 0;

	int enemyDamage;

	void Start(){
		enemyDamage = GameObject.FindGameObjectWithTag("BattleScript").GetComponent<BattleScript>().EnemyDamage;
	}

	void Update(){
		timer += Time.deltaTime;
		--counter;

		if(timer >= timerLimitor)
			Destroy(gameObject);
	}

	void OnGUI(){
		GUI.Label(new Rect((Screen.width / 2 + x), (Screen.height / 2 + y + counter), 50, 50), "<color=red>" + "-" + enemyDamage.ToString() + "</color>");
	}
}
