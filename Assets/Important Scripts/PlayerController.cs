using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed = 1.0f;
	
	private Vector2 pos;
	private bool moving = false;
	
	void Start(){
		// First store our current position when the
		// script is initialized.
		pos = transform.position;
		SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.DOWN;
		SpriteAnimation.isStandingStill = true;
	}
	
	void Update(){
		
		CheckInput();
		
		if (moving) {
			// pos is changed when there's input from the player
			transform.position = pos;
		} 
		else {
			SpriteAnimation.isStandingStill = true;
		}
		
	}
	
	private void CheckInput() {
		SpriteAnimation.isStandingStill = false;
		
		// WASD control
		// We add the direction to our position,
		// this moves the character 1 unit (32 pixels)
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			pos += Vector2.right * speed;
			SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.RIGHT;
			moving = true;
		}
		
		// For left, we have to subtract the direction
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			pos -= Vector2.right * speed;
			SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.LEFT;
			moving = true;
		}
		else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			pos += Vector2.up * speed;
			SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.UP;
			moving = true;
		}
		
		// Same as for the left, subtraction for down
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			pos -= Vector2.up * speed;
			SpriteAnimation.currentTravelDirection = SpriteAnimation.travelDirection.DOWN;
			moving = true;
		}
		else{
			moving = false;
		}
	}
	
	
	
	
}












