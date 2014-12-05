using UnityEngine;
using System.Collections;

public class GameOverGUI : MonoBehaviour {

    public int x = 0;
    public int y = 0;
    void OnGUI()
    {
        const int buttonWidth = 100;
        const int buttonHeight = 60;

        // Determine the button's place on screen
        // Center in x, 2/3 of the height in Y
        Rect buttonRect = new Rect(
            Screen.width / 2 - (buttonWidth / 2),
            (2 * Screen.height / 3) - (buttonHeight / 2),
            buttonWidth, buttonHeight);

        //draw the title
        GUI.Label(new Rect(x, y, 230, 200), "<color=red><size=40>Game Over</size></color>");

        // Draw button to restart the game
        if (GUI.Button(buttonRect, "Retry?"))
        {
            // Load first level on click
            //Level1 is a test name
            Application.LoadLevel("testScene3");
        }
    }
}
