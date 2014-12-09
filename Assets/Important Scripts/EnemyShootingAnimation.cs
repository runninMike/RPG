using UnityEngine;
using System.Collections;

public class EnemyShootingAnimation : MonoBehaviour {
	public Sprite[] sprites;

	private SpriteRenderer spriteRenderer;

	float timer = 0.0f;

	float timeBetweenFrame = 0.125f;

	int index = 1;

	public bool startAnimation = false;

	// Use this for initialization
	void Start (){
		spriteRenderer = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update (){
		if(startAnimation){
			timer += Time.deltaTime;

			if(timer >= timeBetweenFrame){
				if(index < sprites.Length){
					spriteRenderer.sprite = sprites[index++];
					timer = 0.0f;
				}
				else{
					Debug.Log(gameObject.name + " hit player");
					// player got hit
					GameObject obj = Resources.Load<GameObject>("HeroDamageAnimator");					
					obj.GetComponent<HeroDamageAnimator>().heroDamage = GameObject.FindGameObjectWithTag("BattleScript").GetComponent<BattleScript>().HeroDamage;
					Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                    
                    // play sfx
					GameObject.FindGameObjectWithTag("BattleScript").GetComponent<BattleScript>().playBattleSfx();

					timer = 0.0f;
					index = 1;
					startAnimation = false;
				}
			}
		}// start animation
		else{
			spriteRenderer.sprite = sprites[0];
		}
	}

}
