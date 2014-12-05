using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	private bool showInventory = false;
    public GUISkin Menu;

	void Update()
    {
		if (Input.GetKeyDown (KeyCode.I))
			showInventory = !showInventory;
	}
	//the gui for Inventory
	void OnGUI () 
    {
        GUI.skin = Menu;

		if (showInventory)
        {
            DrawInventory();
        }
	}

	void DrawInventory()
    {
		GUI.Box(new Rect((Screen.width - (Screen.width / 2)), (Screen.height - (Screen.height / 2)), 200, 300), "Samsung");

        GUI.color = Color.red;
        GUI.Label(new Rect(((Screen.width - (Screen.width / 2) + 76)), (Screen.height - ((Screen.height / 2) - 60)), 200, 200), "Whiskey: " + Stats.whiskey);
        GUI.Label(new Rect(((Screen.width - (Screen.width / 2) + 76)), (Screen.height - ((Screen.height / 2) - 80)), 200, 200), "Health: " + Stats.health);
	}
}
