using UnityEngine;
using System.Collections;

using TravelDirection = SpriteAnimation.travelDirection;

public class AiNpcController : MonoBehaviour {

	Vector2 position;
	float speed = 1.0f;		
	
	// control movement direction
	public float minSecond = 2.0f;
	public float maxSecond = 5.0f;
	float travelDirectionTimer;
	float travelTime = 0.0f;

	float objectStartTime; 

	TravelDirection direction;

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
		gameObject.GetComponent<SpriteAnimation>().currentTravelDirection = TravelDirection.DOWN;
		gameObject.GetComponent<SpriteAnimation>().isStandingStill = true;

		Random.seed = System.DateTime.Now.Millisecond;
		travelDirectionTimer = Random.Range(minSecond, maxSecond);
		travelTime += travelDirectionTimer + 1.0f;

		///objectStartTime = Time.timeScale;
	}

	void Update(){

		travelTime += Time.deltaTime;

		if(travelTime >= travelDirectionTimer){
			gameObject.GetComponent<SpriteAnimation>().isStandingStill = false;

			// roll 
			GameObject.FindGameObjectWithTag("NpcMoveRoll").GetComponent<NpcMoveRoll>().Roll();
			switch(GameObject.FindGameObjectWithTag("NpcMoveRoll").GetComponent<NpcMoveRoll>().RollResult){
				case 1:		
					direction = TravelDirection.UP;
					break;

				case 3:
					direction = TravelDirection.DOWN;
					break;

				case 5:
					direction = TravelDirection.LEFT;
					break;

				case 7:
					direction = TravelDirection.RIGHT;
					break;

				default:
					direction = TravelDirection.STAND;
					break;
			}

			Random.seed = System.DateTime.Now.Millisecond;
			travelDirectionTimer = Random.Range(minSecond, maxSecond);
			travelTime = 0.0f;
		}

		// see if ghost trigger any collision detector
		GetComponentInChildren<AIGhostController>().UpdateMovement(direction);	
	
		if(GetComponentInChildren<AIGhostController>().isCollisionTrigger){
			//Debug.Log("hey you hit something. turn around");
            // reverse movement
            if (direction == TravelDirection.RIGHT)
            {
                position -= (Vector2.right + (new Vector2(1.0f, 0.0f))) * speed;
                direction = TravelDirection.LEFT;
                processMovement(direction, position);
            }
            else if (direction == TravelDirection.LEFT)
            {
                position += (Vector2.right + (new Vector2(1.0f, 0.0f))) * speed;
                direction = TravelDirection.RIGHT;
                processMovement(direction, position);
            }
            else if (direction == TravelDirection.UP)
            {
                position -= (Vector2.up + (new Vector2(0.0f, 1.0f))) * speed;
                direction = TravelDirection.DOWN;
                processMovement(direction, position);
            }
            else if (direction == TravelDirection.DOWN)
            {
                position += (Vector2.up + (new Vector2(0.0f, 1.0f))) * speed;
                direction = TravelDirection.UP;
                processMovement(direction, position);
            }
            else if (direction == TravelDirection.STAND)
            {
                processMovement(direction, position);
            }
		}
		else{
			UpdatePosition();
		}
	}	
		
	// helpder method
	void UpdatePosition(){
		if(direction == TravelDirection.RIGHT) {
			position += Vector2.right * speed;
			processMovement(direction, position);
		}
		else if(direction == TravelDirection.LEFT) {
			position -= Vector2.right * speed;
			processMovement(direction, position);
		}
		else if(direction == TravelDirection.UP) {
			position += Vector2.up * speed;
			processMovement(direction, position);
		}
		else if(direction == TravelDirection.DOWN) {
			position -= Vector2.up * speed;
			processMovement(direction, position);
		}
		else if(direction == TravelDirection.STAND) {
			processMovement(direction, position);
		}
	}

	public void processMovement(TravelDirection dir, Vector2 newPosition){
		if (dir == TravelDirection.STAND)
			gameObject.GetComponent<SpriteAnimation>().isStandingStill = true;
		else{			
			gameObject.GetComponent<SpriteAnimation>().currentTravelDirection = dir;
			gameObject.GetComponent<SpriteAnimation>().isStandingStill = false;
		}

		transform.position = newPosition;
	}

}
