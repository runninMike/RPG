using UnityEngine;
using System.Collections;

public class AiEnemyController : MonoBehaviour{

	public enum Duty{ GUARD, PATROL, ATEASE };
	public Duty dutyTitle;
	public enum PATH{ HORIZONTAL, VERTICAL, NONE };
	public PATH patrolPath;

	Vector2 position;
	float speed = 1.0f;		
	
	// control movement direction
	public float minSecond = 2.0f;
	public float maxSecond = 5.0f;
	float travelDirectionTimer;
	float travelTime = 0.0f;

	float objectStartTime;

	SpriteAnimation.travelDirection direction;

	public float Speed{
		get{ return speed; }
		set{ 
			speed = value;
			GetComponentInChildren<AiEnemyGhostController>().speed = value;
		}
	}

	void Start(){
		// First store our current position when the
		// script is initialized.
		position = transform.position;
		gameObject.GetComponent<SpriteAnimation>().currentTravelDirection = SpriteAnimation.travelDirection.DOWN;
		gameObject.GetComponent<SpriteAnimation>().isStandingStill = true;

		Random.seed = System.DateTime.Now.Millisecond;
		travelDirectionTimer = Random.Range(minSecond, maxSecond);
		travelTime += travelDirectionTimer + 1.0f;

		if(dutyTitle == Duty.PATROL){
			minSecond = 3.0f;
			maxSecond = 7.0f;
		}

		objectStartTime = Time.unscaledTime;
	}

	void Update(){
		if(dutyTitle == Duty.ATEASE)
			NormalMovement();
		else if(dutyTitle == Duty.GUARD)
			GuardMovement();
		else if(dutyTitle == Duty.PATROL)
			PatrolMovement();	
	}

	void NormalMovement(){
		travelTime += (Time.unscaledTime - objectStartTime) / 1000.0f;

		if(travelTime >= travelDirectionTimer){
			gameObject.GetComponent<SpriteAnimation>().isStandingStill = false;

			// roll 
			GameObject.FindGameObjectWithTag("NpcMoveRoll").GetComponent<NpcMoveRoll>().Roll();
			switch(GameObject.FindGameObjectWithTag("NpcMoveRoll").GetComponent<NpcMoveRoll>().RollResult){
				case 1:		
					direction = SpriteAnimation.travelDirection.UP;
					break;

				case 3:
					direction = SpriteAnimation.travelDirection.DOWN;
					break;

				case 5:
					direction = SpriteAnimation.travelDirection.LEFT;
					break;

				case 7:
					direction = SpriteAnimation.travelDirection.RIGHT;
					break;

				default:
					direction = SpriteAnimation.travelDirection.STAND;
					break;
			}

			Random.seed = System.DateTime.Now.Millisecond;
			travelDirectionTimer = Random.Range(minSecond, maxSecond);
			travelTime = 0.0f;
		}

		UpdateGhost();
	}

	void GuardMovement(){
		gameObject.GetComponent<SpriteAnimation>().isStandingStill = true;
	}	
	
	void PatrolMovement(){
		travelTime += Time.deltaTime;

		if(travelTime >= travelDirectionTimer){
			gameObject.GetComponent<SpriteAnimation>().isStandingStill = false;

			// roll 
			GameObject.FindGameObjectWithTag("NpcMoveRoll").GetComponent<NpcMoveRoll>().Roll();
			switch(GameObject.FindGameObjectWithTag("NpcMoveRoll").GetComponent<NpcMoveRoll>().RollResult){
				case 1:	
					if(patrolPath == PATH.VERTICAL)
						direction = SpriteAnimation.travelDirection.UP;
					if(patrolPath == PATH.HORIZONTAL)
						direction = SpriteAnimation.travelDirection.RIGHT;
					break;

				case 3:
					if(patrolPath == PATH.VERTICAL)
						direction = SpriteAnimation.travelDirection.DOWN;
					if(patrolPath == PATH.HORIZONTAL)
						direction = SpriteAnimation.travelDirection.LEFT;
					break;

				case 5:
					if(patrolPath == PATH.VERTICAL)
						direction = SpriteAnimation.travelDirection.UP;
					if(patrolPath == PATH.HORIZONTAL)
						direction = SpriteAnimation.travelDirection.RIGHT;
					break;

				case 7:
					if(patrolPath == PATH.VERTICAL)
						direction = SpriteAnimation.travelDirection.DOWN;
					if(patrolPath == PATH.HORIZONTAL)
						direction = SpriteAnimation.travelDirection.LEFT;
					break;

				default:
					direction = SpriteAnimation.travelDirection.STAND;
					break;
			}

			Random.seed = System.DateTime.Now.Millisecond;
			travelDirectionTimer = Random.Range(minSecond, maxSecond);
			travelTime = 0.0f;
		}

		UpdateGhost();
	}

	// helper method
	void UpdateGhost(){
		// see if ghost trigger any collision detector
		GetComponentInChildren<AiEnemyGhostController>().UpdateMovement(direction);	
	
		if(GetComponentInChildren<AiEnemyGhostController>().isCollisionTrigger){
			// reverse movement
			if(direction == SpriteAnimation.travelDirection.RIGHT) {
				position -= Vector2.right * speed;
				direction = SpriteAnimation.travelDirection.LEFT;
				processMovement(direction, position);
			}
			else if(direction == SpriteAnimation.travelDirection.LEFT) {
				position += Vector2.right * speed;
				direction = SpriteAnimation.travelDirection.RIGHT;
				processMovement(direction, position);
			}
			else if(direction == SpriteAnimation.travelDirection.UP) {
				position -= Vector2.up * speed;
				direction = SpriteAnimation.travelDirection.DOWN;
				processMovement(direction, position);
			}
			else if(direction == SpriteAnimation.travelDirection.DOWN) {
				position += Vector2.up * speed;
				direction = SpriteAnimation.travelDirection.UP;
				processMovement(direction, position);
			}
			else if(direction == SpriteAnimation.travelDirection.STAND) {
				processMovement(direction, position);
			}

			//GetComponentInChildren<AiEnemyGhostController>().isCollisionTrigger = false;
		}
		else{
			if(direction == SpriteAnimation.travelDirection.RIGHT) {
				position += Vector2.right * speed;
				processMovement(direction, position);
			}
			else if(direction == SpriteAnimation.travelDirection.LEFT) {
				position -= Vector2.right * speed;
				processMovement(direction, position);
			}
			else if(direction == SpriteAnimation.travelDirection.UP) {
				position += Vector2.up * speed;
				processMovement(direction, position);
			}
			else if(direction == SpriteAnimation.travelDirection.DOWN) {
				position -= Vector2.up * speed;
				processMovement(direction, position);
			}
			else if(direction == SpriteAnimation.travelDirection.STAND) {
				processMovement(direction, position);
			}
		}
	}
		
	public void processMovement(SpriteAnimation.travelDirection dir, Vector2 newPosition){
		if (dir == SpriteAnimation.travelDirection.STAND)
			gameObject.GetComponent<SpriteAnimation>().isStandingStill = true;
		else{			
			gameObject.GetComponent<SpriteAnimation>().currentTravelDirection = dir;
			gameObject.GetComponent<SpriteAnimation>().isStandingStill = false;
		}

		transform.position = newPosition;
	}

}
