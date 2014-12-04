using UnityEngine;
using System.Collections;

public class AiNpcController : MonoBehaviour {

	Vector2 position;
	float speed = 1.0f;		
	
	// control movement direction
	public float minSecond = 2.0f;
	public float maxSecond = 5.0f;
	float travelDirectionTimer;
	float travelTime = 0.0f;

	SpriteAnimation.travelDirection direction;

	public float Speed{
		get{ return speed; }
		set{ 
			speed = value;
			GetComponentInChildren<AIGhostController>().speed = value;
		}
	}

	void Start(){
		// First store our current position when the
		// script is initialized.
		position = transform.position;
		SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.DOWN;
		SpriteAnimation.isStandingStill = true;

		Random.seed = System.DateTime.Now.Millisecond;
		travelDirectionTimer = Random.Range(minSecond, maxSecond);
		travelTime += travelDirectionTimer + 1.0f;
	}

	void Update(){
		travelTime += Time.deltaTime;

		if(travelTime >= travelDirectionTimer){
			SpriteAnimation.isStandingStill = false;

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

		// see if ghost trigger any collision detector
		GetComponentInChildren<AIGhostController>().UpdateMovement(direction);	
	
		if(GetComponentInChildren<AIGhostController>().isCollisionTrigger){
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
			SpriteAnimation.isStandingStill = true;
		else{			
			SpriteAnimation.currentTravelDirection = dir;
			SpriteAnimation.isStandingStill = false;
		}

		transform.position = newPosition;
	}

}
