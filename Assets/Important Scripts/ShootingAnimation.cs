using UnityEngine;
using System.Collections;

public class ShootingAnimation : MonoBehaviour {

	public Sprite[] sprites;

	private SpriteRenderer spriteRenderer;

	float timer = 0.0f;

	int index = 1;

	// coodinates for damage indicator animation
	int x_e1hl = -30;		// x enemy 1 hit label
	int y_e1hl = -40;		// y enemy 1 hit label
	int x_e2hl = 53;		// x enemy 2 hit label  
	int y_e2hl = -40;		// y enemy 2 hit label  note: to go up it's minus y

	// Use this for initialization
	void Start (){
		spriteRenderer = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update (){
		timer += Time.deltaTime;

		if(timer >= 0.1f){
			if(index < sprites.Length){
				spriteRenderer.sprite = sprites[index++];
				timer = 0.0f;
			}
			else{
				// enermy got hit, play damage indicator
				GameObject obj = Resources.Load<GameObject>("EnemyDamageAnima");

				if(GameObject.Find("Battle").GetComponent<BattleScript>().WhichEnemyGotShot == BattleScript.WhichEnemy.ONE){
					obj.GetComponent<EnemyDamageAnimation>().x = x_e1hl;
					obj.GetComponent<EnemyDamageAnimation>().y = y_e1hl;
				}
				else if(GameObject.Find("Battle").GetComponent<BattleScript>().WhichEnemyGotShot == BattleScript.WhichEnemy.TWO){
					obj.GetComponent<EnemyDamageAnimation>().x = x_e2hl;
					obj.GetComponent<EnemyDamageAnimation>().y = y_e2hl;
				}
				obj.GetComponent<EnemyDamageAnimation>().enemyDamage = GameObject.Find("Battle").GetComponent<BattleScript>().EnemyDamage;
				Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

				GameObject.Find("Battle").GetComponent<BattleScript>().timerOn = true;
				Destroy(gameObject);
			}
		}
	}
}
