using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour{
	public Sprite[] sprites;
	public float fps;
	public int framesPerAnimation;			// number of frames per animation sequence

	public bool isStandingStill = false;

	// the sprite current travling direction
	public enum travelDirection{ DOWN, LEFT, RIGHT, UP, STAND};
	public travelDirection currentTravelDirection;  

	// enum based on how unity split the spritesheet
	// to get the correct movement sprite, unity split spritesheet into a single dimension array
	// Make sure each enum matches the direction of each sprite index
	enum offset{ DOWN, LEFT, RIGHT, UP };
	offset frameOffset;

	private SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start (){
		spriteRenderer = renderer as SpriteRenderer;
	}

	// Update is called once per frame
	void Update (){
		if (!isStandingStill) {
			int index = (int)(Time.timeSinceLevelLoad * fps);
			index = index % framesPerAnimation;

			switch(currentTravelDirection){
				case travelDirection.DOWN:
					frameOffset = offset.DOWN;
					break;

				case travelDirection.LEFT:
					frameOffset = offset.LEFT;
					break;
					
				case travelDirection.RIGHT:
					frameOffset = offset.RIGHT;
					break;
					
				case travelDirection.UP:
					frameOffset = offset.UP;
					break;
			}

			spriteRenderer.sprite = sprites [index + ((int)frameOffset * framesPerAnimation)];
		}
		else{
			if(currentTravelDirection == travelDirection.DOWN)
				spriteRenderer.sprite = sprites[1];
			else if(currentTravelDirection == travelDirection.LEFT)
				spriteRenderer.sprite = sprites[4];
			else if(currentTravelDirection == travelDirection.RIGHT)
				spriteRenderer.sprite = sprites[7];
			else if(currentTravelDirection == travelDirection.UP)
				spriteRenderer.sprite = sprites[10];
		}
	}
}

