using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	Vector2 position;
	float speed = 1.0f;	
	
	public float Speed{
		get{ return speed; }
		set{ 
			speed = value;
			GetComponentInChildren<GhostController>().speed = value;
		}
	}

	void Start(){
		// First store our current position when the
		// script is initialized.
		position = transform.position;
		SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.DOWN;
		SpriteAnimation.isStandingStill = true;
	}

	void Update(){
		GetComponentInChildren<GhostController>().UpdateMovement();	
	
		if(!GetComponentInChildren<GhostController>().isCollisionTrigger)
			ProcessInput();
		else
			stopMovement();
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
		else{
			stopMovement();
		}
	}
	

	void stopMovement(){
		SpriteAnimation.isStandingStill = true;
	}
		
	public void allowMovement(KeyCode keyPressed, Vector2 newPosition){
		if (keyPressed == KeyCode.D){
			SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.RIGHT;
		}
		else if (keyPressed == KeyCode.A){
			SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.LEFT;
		}
		else if (keyPressed == KeyCode.W){
			SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.UP;
		}		
		else if (keyPressed == KeyCode.S){
			SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.DOWN;
		}
		SpriteAnimation.isStandingStill = false;

		transform.position = newPosition;
	}


}












