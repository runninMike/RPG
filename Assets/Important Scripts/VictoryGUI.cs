using UnityEngine;
using System.Collections;

public class VictoryGUI : MonoBehaviour {

	public int x = 0;
    public int y = 0;
    public int x1 = 0;
    public int y1 = 0;
    public int x2 = 0;
    public int y2 = 0;
    public int x3 = 0;
    public int y3 = 0;

    void OnGUI()
    {

        // Determine the button's place on screen
        // Center in x, 2/3 of the height in Y
        Rect buttonRect = new Rect(((Screen.width - (Screen.width / 2) + x3)), (Screen.height - ((Screen.height / 2) - y3)), 200, 100);

        //draw the title
        GUI.Label(new Rect(x, y, 230, 200), "<color=red><size=40>Victory!</size></color>");

        //show what the player gained
        GUI.Box(new Rect((Screen.width - ((Screen.width / 2) + x1)), (Screen.height - ((Screen.height / 2) + y1)), 200, 300), "You gained:");

        GUI.color = Color.red;
        GUI.Label(new Rect(((Screen.width - (Screen.width / 2) + x2)), (Screen.height - ((Screen.height / 2) - y2)), 200, 200), "Whiskey: +2");
        GUI.Label(new Rect(((Screen.width - (Screen.width / 2) + x2)), (Screen.height - ((Screen.height / 2) - y2 - 20)), 200, 200), "Experience: +100");

        // Draw button to restart the game
        if (GUI.Button(buttonRect, "Continue"))
        {
            // Load first level on click
            //Level1 is a test name
            Application.LoadLevel("testScene3");
        }

    }
}
