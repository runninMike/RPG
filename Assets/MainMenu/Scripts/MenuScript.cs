using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour 
{
	void OnGUI()
	{
		const int buttonWidth = 84;
		const int buttonHeight = 60;

		// Determine the button's place on screen
		// Center in x, 2/3 of the height in Y
		Rect buttonRect = new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth, buttonHeight);

		Rect buttonRect2 = new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 4) - (buttonHeight / 2),
			buttonWidth, buttonHeight);

		// Draw button to start the game
		if (GUI.Button (buttonRect, "Battle!"))
		{
			// Load first level on click
			//Level1 is a test name
			Application.LoadLevel("BattleTest");
		}

		if (GUI.Button (buttonRect2, "Town!"))
		{
			// Load first level on click
			//Level1 is a test name
			Application.LoadLevel("testScene2");
		}
	}
}
