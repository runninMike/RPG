using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleScript : MonoBehaviour
{
    bool isPlayerTurn = true;
    int roll1 = 0;
    int roll2 = 0;
    const int buttonWidth = 84;
    const int buttonHeight = 60;
    static int playerHP = 100;
    int enemyHP = 50;

    public int x = Screen.width / 2 - (buttonWidth / 2);
    public int y = (2 * Screen.height / 4) - (buttonHeight / 2);
    public int x2 = Screen.width / 2 - (buttonWidth / 2);
    public int y2 = (2 * Screen.height / 4) - (buttonHeight / 2);
    public int x3 = Screen.width / 2 - (buttonWidth / 2);
    public int y3 = (2 * Screen.height / 4) - (buttonHeight / 2);

    void OnGUI()
    {
        //*************************************COMBAT*****************************************
        // Determine the button's place on screen
        // Center in x, 2/3 of the height in Y
        Rect buttonRect = new Rect(
            x, y,
            buttonWidth, buttonHeight);

        Rect buttonRect2 = new Rect(
            x2, y2,
            buttonWidth, buttonHeight);

        Rect enemyRect1 = new Rect(
            x3, y3,
            buttonWidth, buttonHeight);


        // Draw button to attack
        if (GUI.Button(buttonRect, "Attack"))
        {
            while (isPlayerTurn)
            {
                //this is the players dice roll
                GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                roll1 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
                Debug.Log("player: " + roll1);

                //enemy's dice roll
                GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                roll2 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
                Debug.Log("enemy: " + roll2);

                //checks if player hits or not
                if (roll1 >= roll2)
                    enemyHP -= 25;

                else
                    Debug.Log("You missed!");

                isPlayerTurn = false;
            }
        }

        //enemy turn
        while (!isPlayerTurn)
        {

			//enemy's dice roll
            GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
            roll2 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
            Debug.Log("enemy: " + roll2);

            //this is the players dice roll
            GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
            roll1 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
            Debug.Log("player: " + roll1);
			            
            //checks if enemy hits or not
            if (roll2 >= roll1)
                playerHP -= 10;

            else
                 Debug.Log("Enemy missed!");

            isPlayerTurn = true;
        }

        //use item, end turn
        if (GUI.Button(buttonRect2, "Whiskey"))
        {
            //heals the player
            playerHP += 25;
            isPlayerTurn = false;
        }

    //******************************************GUI BOX*******************************************

            
    }
}
