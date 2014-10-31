using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed = 1.0f;	

	bool isColliding = false;
	
	void Start(){
		// First store our current position when the
		// script is initialized.
		pos = transform.position;
		SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.DOWN;
		SpriteAnimation.isStandingStill = true;
	}

	void Update(){
		CheckInput();

		// allow collider event to happen.

		/*
		if(isCollWithDoor){
			transform.position = previousPos;
			moving = false;
		}

		if (moving) {
			// pos is changed when there's input from the player
			transform.position = pos;
		} 
		else {
			SpriteAnimation.isStandingStill = true;
		}
		*/	
		if(!isColliding)
			allowMovement();

		//lets the player move in any other direction if stuck, but it's crazy
		//glitchy, there's probably a better way of handling this.
		else if (isColliding && SpriteAnimation.currentTravelDirection != currentTravelDirection)
			allowMovement ();
	}




	SpriteAnimation.travelDirection currentTravelDirection;
	private Vector2 pos;
	private Vector2 previousPos;
	private bool moving = false;

	private void CheckInput() {
		//SpriteAnimation.isStandingStill = false;
		previousPos = transform.position;
		
		// WASD control
		// We add the direction to our position,
		// this moves the character 1 unit (32 pixels)
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			pos += Vector2.right * speed;
			currentTravelDirection = SpriteAnimation.travelDirection.RIGHT;
			moving = true;
		}
		
		// For left, we have to subtract the direction
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			pos -= Vector2.right * speed;
			currentTravelDirection = SpriteAnimation.travelDirection.LEFT;
			moving = true;
		}
		else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			pos += Vector2.up * speed;
			currentTravelDirection = SpriteAnimation.travelDirection.UP;
			moving = true;
		}
		
		// Same as for the left, subtraction for down
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			pos -= Vector2.up * speed;
			currentTravelDirection = SpriteAnimation.travelDirection.DOWN;
			moving = true;
		}
		else{
			moving = false;
		}
	}


	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Door"){

			stopMovement();
			Debug.Log ("STOP!");
		}
	}

	void OnCollisionExit2D(Collision2D col){
		isColliding = false;
		//if(col.gameObject.tag == "Door"){
			allowMovement();
			Debug.Log("moving along");
		//}
	}


	void stopMovement(){
		isColliding = true;
		transform.position = previousPos;
		SpriteAnimation.isStandingStill = true;
	}
		
	void allowMovement(){
		isColliding = false;
		transform.position = pos;
		SpriteAnimation.currentTravelDirection = currentTravelDirection;
		SpriteAnimation.isStandingStill = !moving;
	}
}












