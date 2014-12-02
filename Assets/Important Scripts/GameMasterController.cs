using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMasterController : MonoBehaviour {

    int enemyDie = 0;
    int playerDie = 0;

    bool isPlayerTurn = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 
	}

    void OnGUI()
    {           
        GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
        playerDie = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
 

        // enermy throw
        // calculation
        // hp 

        // playerTurn off.
    }

    void Turn()
    {
        // enermy turn , delete gui. 
        if (!isPlayerTurn)
        {
            // enermy thrown first
            // player throw second
            // calatioh
            // hp calucation

        }
        isPlayerTurn = true;
        // playerTurn, turn on gui.

    }
}
