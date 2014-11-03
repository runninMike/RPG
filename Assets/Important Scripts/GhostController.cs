using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {
	
	public float speed = 1.0f;	
	public Vector2 position;

	public bool isCollisionTrigger = false;
	
	void Start(){
		// First store our current position when the
		// script is initialized.
		position = transform.position;
		speed = this.transform.parent.GetComponent<PlayerController>().Speed;
	}

	void Update(){}

	KeyCode keyPressed;
	public void UpdateMovement(){
		position = this.transform.parent.transform.position;

		// get next movement and update transform so trigger events are checked
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			position += Vector2.right * speed;
			keyPressed = KeyCode.D;
		}		
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			position -= Vector2.right * speed;
			keyPressed = KeyCode.A;
		}
		else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			position += Vector2.up * speed;
			keyPressed = KeyCode.W;
		}
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			position -= Vector2.up * speed;
			keyPressed = KeyCode.S;
		}
		
		transform.position = position;
	}


	void OnTriggerEnter2D(Collider2D col){
		isCollisionTrigger = true;
	}

	void OnTriggerExit2D(Collider2D col){
		GetComponentInParent<PlayerController>().allowMovement(keyPressed, transform.position);
		isCollisionTrigger = false;
	}		

}












