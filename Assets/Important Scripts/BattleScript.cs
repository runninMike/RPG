using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleScript : MonoBehaviour
{
    bool isPlayerTurn = true;
    int roll1 = 0;
    int roll2 = 0;
    int enemyCount = 0;
    const int buttonWidth = 130;
    const int buttonHeight = 100;
    static int playerHP = 100;
    int[] enemyHP = {50 , 50};
    static int whiskey = 4;

    //******************************************************************
    public int x = 0;
    public int y = 0;
    public int x2 = 0;
    public int y2 = 0;
    public int x3 = 0;
    public int y3 = 0;
    public int x4 = 0;
    public int y4 = 0;
    public int x5 = 0;
    public int y5 = 0;
    //******************************************************************
    void Start()
    {
        //this is the enemy number dice roll
        GameObject.FindGameObjectWithTag("D4").GetComponent<D4>().Roll();
        switch(GameObject.FindGameObjectWithTag("D4").GetComponent<D4>().RollResult){
            case 1:
            case 2:
                enemyCount = 1;
                break;

            case 3: 
            case 4:
                 enemyCount = 2;
                break;
        } 

        
    }

    void Update()
    {
        if (enemyHP[0] <= 0 && enemyHP[1] <= 0)
        {
            Application.LoadLevel("testScene3");
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(x5,y5, 100, 100), "dsklfdsklf");
        //*************************************COMBAT*****************************************
        // Determine the button's place on screen
        // Center in x, 2/3 of the height in Y
        Rect enemy1Rect = new Rect(
            (Screen.width / 2 + x), (Screen.height / 2 + y),
            buttonWidth, buttonHeight);

        Rect enemy2Rect = new Rect(
            (Screen.width / 2 + x2), (Screen.height / 2 + y2),
            buttonWidth, buttonHeight);

        Rect whiskeyRect = new Rect(
            (Screen.width / 2 + x3), (Screen.height / 2 + y3),
            buttonWidth, buttonHeight);

        Rect runRect1 = new Rect(
            (Screen.width / 2 + x4), (Screen.height / 2 + y4),
            buttonWidth, buttonHeight);


        // Draw button to attack
        if (GUI.Button(enemy1Rect, "Attack\nEnemy 1 HP:" + enemyHP[0]))
        {
            if (isPlayerTurn)
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
                    enemyHP[0] -= 25;

                else
                    Debug.Log("You missed!");

                isPlayerTurn = false;
            }
        }

        if (enemyCount > 0)
        {
            if (GUI.Button(enemy2Rect, "Attack\nEnemy 2 HP:" + enemyHP[1]))
            {
                if (isPlayerTurn)
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
                        enemyHP[1] -= 25;

                    else
                        Debug.Log("You missed!");

                    isPlayerTurn = false;
                }
            }
        }

        //enemy turn
        if (!isPlayerTurn)
        {

            for (int i = 0; i < enemyCount; ++i)
            {
                //enemy's dice roll
                GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                roll2 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
                Debug.Log("enemy " + (i + 1) + ":" + roll2);

                //this is the players dice roll
                GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                roll1 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
                Debug.Log("player: " + roll1);

                //checks if enemy hits or not
                if (roll2 >= roll1)
                    playerHP -= 10;

                else
                    Debug.Log("Enemy missed!");
            }
            isPlayerTurn = true;
        }

        //use item, end turn
        if (GUI.Button(whiskeyRect, "Whiskey"))
        {

            //heals the player
            if (whiskey > 0)
            {
                playerHP += 10;
                whiskey--;
            }

            isPlayerTurn = false;
        }
        
        //run button
        if (GUI.Button(runRect1, "Run"))
        {
            if (isPlayerTurn)
            {
                GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                if (GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult == 20)
                {
                    Debug.Log("run");  // end battle player get's nothing and  enemy is destoryed.  
                    Application.LoadLevel("testScene3");
                }               
                isPlayerTurn = false;
            }
        }
    }
}
