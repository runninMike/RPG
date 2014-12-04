using UnityEngine;
using System.Collections;

public class AiNpcController : MonoBehaviour {

	Vector2 position;
	float speed = 1.0f;	
	
	
	// control movement direction
	public float minSecond = 1.0f;
	public float maxSecond = 2.0f;
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

		}

		GetComponentInChildren<AIGhostController>().UpdateMovement();	
	
		if(GetComponentInChildren<AIGhostController>().isCollisionTrigger)
			ReverseMovement();
		else
			ProcessInput();

	}
	
	private void ProcessInput(){		
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			position += Vector2.right * speed;
			allowMovement(KeyCode.D, position);
		}
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			position -= Vector2.right * speed;
			allowMovement(KeyCode.A, position);
		}
		else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			position += Vector2.up * speed;
			allowMovement(KeyCode.W, position);
		}
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			position -= Vector2.up * speed;
			allowMovement(KeyCode.S, position);
		}

	}
	

	void ReverseMovement(){
		SpriteAnimation.isStandingStill = true;
	}
		
	public void allowMovement(SpriteAnimation.travelDirection dir, Vector2 newPosition){
		if (dir == SpriteAnimation.travelDirection.STAND)
			SpriteAnimation.isStandingStill = false;
		else			
			SpriteAnimation.currentTravelDirection = dir;

		transform.position = newPosition;
	}

}
