using UnityEngine;
using System.Collections;

public class AiEnemyGhostController : MonoBehaviour {
	public float speed = 1.0f;	
	public Vector2 position;

	public bool isCollisionTrigger = false;
	
	void Start(){
		// First store our current position when the
		// script is initialized.
		position = transform.position;
		speed = this.transform.parent.GetComponent<AiEnemyController>().Speed;
	}
	

	public void UpdateMovement(SpriteAnimation.travelDirection direction){		
		position = this.transform.parent.transform.position;

		// get next movement and update transform so trigger events are checked		
		if(direction == SpriteAnimation.travelDirection.RIGHT) {
			position += Vector2.right * speed;
		}
		else if(direction == SpriteAnimation.travelDirection.LEFT) {
			position -= Vector2.right * speed;
		}
		else if(direction == SpriteAnimation.travelDirection.UP) {
			position += Vector2.up * speed;
		}
		else if(direction == SpriteAnimation.travelDirection.DOWN) {
			position -= Vector2.up * speed;
		}
		
		transform.position = position;
	}


	void OnTriggerEnter2D(Collider2D col){
		isCollisionTrigger = true;

        if (col.tag == GameObject.FindGameObjectWithTag("Hero").tag)
        {
            Destroy(gameObject);
            Application.LoadLevel("BattleTest");
        }
	}

	void OnTriggerExit2D(Collider2D col){
		isCollisionTrigger = false;
	}		
}
