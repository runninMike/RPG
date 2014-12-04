using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public List<Item> inventory = new List<Item>();
	private ItemDatabase database;
	private bool showInventory = false;

	// Use this for initialization
	void Start () 
    {
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase> (); 
		inventory.Add (database.items [0]);
		inventory.Add (database.items [1]);
		print (inventory[0].itemName); 
		print (inventory[1].itemName); 
	}

	void Update()
    {
		if (Input.GetKeyDown (KeyCode.I))
			showInventory = !showInventory;
	}
	//the gui for Inventory
	void OnGUI () 
    {
		if (showInventory) 
			DrawInventory();

		for (int i = 0; i < inventory.Count; i++)
			GUI.Label(new Rect(10, i * 20, 200, 50), inventory[i].itemName);
	}

	void DrawInventory()
    {
		for (int x = 0; x < 10; x++)
			GUI.Box(new Rect(20, x * 20, 20, 20), "Inventory");	
	}
}
