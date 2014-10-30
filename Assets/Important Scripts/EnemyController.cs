using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour{
	// how many enermies are there in the spritesheet
	public int numberOfEnermy;
	int currentEnemySprite;

	public Sprite[] sprites;

	SpriteRenderer spriteRenderer;
	enum battlePosition{ STAND_BY, READY, FIRE };
	battlePosition currentBattlePosition;

	int spriteOffset = 0;

	float timer = 0.0f;
	float timeLimit = 1.0f;

	// Use this for initialization
	void Start () {
		spriteRenderer = renderer as SpriteRenderer;
		currentEnemySprite = Random.Range(0, numberOfEnermy);
		currentBattlePosition = battlePosition.STAND_BY;
	}

	// Update is called once per frame
	void Update (){
		spriteRenderer.sprite = sprites[currentEnemySprite + (int)currentBattlePosition];

		timer += Time.deltaTime;
		if(timer >= timeLimit){
			switch(currentBattlePosition){
				case battlePosition.STAND_BY:
					currentBattlePosition = battlePosition.READY;
					break;
				case battlePosition.READY:
					currentBattlePosition = battlePosition.FIRE;
					break;
				case battlePosition.FIRE:
					int temp;
					do{
						temp = Random.Range (0, numberOfEnermy);
					}while(temp == currentEnemySprite);
					currentEnemySprite = temp;

					currentBattlePosition = battlePosition.STAND_BY;
					break;
			}
			timer = 0.0f;
		}// end timer
	}

}
