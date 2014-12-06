using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour 
{
    public int x = 0;
    public int y = 0;
    public AudioClip menuPress;
    bool soundPlayed = false;
    string hover;

    void Update()
    {
        if (hover == "Battle" && !soundPlayed)
        {
            soundPlayed = true;
            audio.PlayOneShot(menuPress);
        }

        if (hover == "") //or whatever it returns when you are not over a button.
        {
            soundPlayed = false;
        }
    }
	void OnGUI()
	{
        hover = GUI.tooltip;
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

        Rect titleRect = new Rect(
            (Screen.width / 2 + x), (Screen.height / 2 + y),
            buttonWidth, buttonHeight);

        //draw the title
      
        GUI.Label(new Rect(x, y, 230, 200),"<color=white><size=40>Fractured</size></color>");

		// Draw button to start the game
		if (GUI.Button (buttonRect, "Battle!"))
		{
            //sound on click
            audio.PlayOneShot(menuPress);

			// Load first level on click
			//Level1 is a test name
			Application.LoadLevel("BattleTest");
		}

		if (GUI.Button (buttonRect2, "Town!"))
		{
            //sound on click
            audio.PlayOneShot(menuPress);

			// Load first level on click
			//Level1 is a test name
			Application.LoadLevel("testScene3");
		}
	}
}
