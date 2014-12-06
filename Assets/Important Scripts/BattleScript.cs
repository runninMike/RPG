using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class BattleScript : MonoBehaviour
{
    bool isPlayerTurn = true;
    int roll1 = 0;
    int roll2 = 0;
    int enemyCount = 0;
    const int buttonWidth = 130;
    const int buttonHeight = 100;
    int[] enemyHP = {0 , 0};		// value set in start()
    int bossHP = 200;

	int enemyDamage = 20;
	int heroDamage = 10;

	int WhiskeyHP = 10;

	public int EnemyDamage{ get{ return enemyDamage; } }

	public enum WhichEnemy{ ONE, TWO };
	WhichEnemy whichEnemyGotShot;
	public WhichEnemy WhichEnemyGotShot{ get{ return whichEnemyGotShot; } }

	// access and assigned this before level start
	public enum BattleType{ NORMAL, BOSS };
	public BattleType battleType;


	// timer for the enemy turn
	float timeLimtior = 0.5f;
	float timer = 0.0f;
	public bool timerOn = false;
	bool enemyTurn = false;

	// determine the button's place on screen
	// better to set once then each time OnGui() is called.
	Rect enemy1Rect, enemy2Rect, whiskeyRect, runRect1;

	Rect labelRect;

	//Rect enemy1HitLbl, enemy2HitLbl;

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

	//int x_e1hl = -30;		// x enemy 1 hit label
	//int y_e1hl = -40;		// y enemy 1 hit label
	//int x_e2hl = 55;		// x enemy 2 hit label  
	//int y_e2hl = -40;		// y enemy 2 hit label  note: to go up it's minus y
    //******************************************************************
    void Start()
    { 		
		if(battleType == BattleType.NORMAL){
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

			enemyCount = 2;

			// there's always one enemy
			enemyHP[0] = 100;
			if(enemyCount == 2)
				enemyHP[1] = 100;
		}
		else if(battleType == BattleType.BOSS){
			enemyCount = 1;
			enemyHP[0] = bossHP;
		}

		Debug.Log("Enemy Count: " + enemyCount);

		// Determine the button's place on screen
		// Center in x, 2/3 of the height in Y
		enemy1Rect = new Rect(
			(Screen.width / 2 + x), (Screen.height / 2 + y),
			buttonWidth, buttonHeight);

		enemy2Rect = new Rect(
			(Screen.width / 2 + x2), (Screen.height / 2 + y2),
			buttonWidth, buttonHeight);

		whiskeyRect = new Rect(
			(Screen.width / 2 + x3), (Screen.height / 2 + y3),
			buttonWidth, buttonHeight);

		runRect1 = new Rect(
			(Screen.width / 2 + x4), (Screen.height / 2 + y4),
			buttonWidth, buttonHeight);  
      
		labelRect = new Rect((Screen.width / 2 + x5), (Screen.height / 2 + y5), 100, 100);

		//enemy1HitLbl = new Rect((Screen.width / 2 + x_e1hl), (Screen.height / 2 + y_e1hl), 50, 50);
		//enemy2HitLbl = new Rect((Screen.width / 2 + x_e2hl), (Screen.height / 2 + y_e2hl), 50, 50);
    }

    void Update(){	
		 //win/lose conditions
        if (enemyHP[0] <= 0 && enemyHP[1] <= 0)
        {
            Application.LoadLevel("Victory");
        }

        if (Stats.health <= 0)
        {
            Application.LoadLevel("GameOver");
        }

		if(timerOn){
			timer += Time.deltaTime;
			if(timer >= timeLimtior){
				enemyTurn = true;
				timerOn = false;
				timer = 0.0f;
			}
		}
    }


    void OnGUI()
    {	
		// "<color=red><size=40>Victory!</size></color>"
		//GUI.Label(new Rect((Screen.width / 2 + x_e1hl), (Screen.height / 2 + y_e1hl), 50, 50), "<color=red>" + "-" + enemyDamage.ToString() + "</color>");
		//GUI.Label(new Rect((Screen.width / 2 + x_e2hl), (Screen.height / 2 + y_e2hl), 50, 50), "<color=red>" + "-" + enemyDamage.ToString() + "</color>");
		GUI.Label(labelRect, "Your turn.");



        //*************************************COMBAT*****************************************
        
        // Draw button to attack
		if(enemyHP[0] > 0){
			if (GUI.Button(enemy1Rect, "Attack\nEnemy 1 HP:" + enemyHP[0]))
			{
				if (isPlayerTurn)
				{
					//this is the players dice roll
					GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
					roll1 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
					//Debug.Log("player: " + roll1);

					//enemy's dice roll
					GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
					roll2 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
					//Debug.Log("enemy: " + roll2);


					//checks if player hits or not
					if (roll1 >= roll2){
						whichEnemyGotShot = WhichEnemy.ONE;
						// play shooting animation
						Instantiate(Resources.Load<GameObject>("Andrea"), 
									new Vector3(GameObject.Find("enemy1").transform.position.x, 
												GameObject.Find("enemy1").transform.position.y, 0.0f),
									Quaternion.identity);
						enemyHP[0] -= enemyDamage;						
					}
                	else{
						timerOn = true;
					}

					isPlayerTurn = false;
				}
			}
		}

		// button 2
        if (enemyCount > 1 && enemyHP[1] > 0)
        {
            if (GUI.Button(enemy2Rect, "Attack\nEnemy 2 HP:" + enemyHP[1]))
            {
                if (isPlayerTurn)
                {
                    //this is the players dice roll
                    GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                    roll1 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
                    //Debug.Log("player: " + roll1);

                    //enemy's dice roll
                    GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                    roll2 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
                   //Debug.Log("enemy: " + roll2);

                    //checks if player hits or not
                    if (roll1 >= roll2){
						whichEnemyGotShot = WhichEnemy.TWO;
						// play shooting animation
						Instantiate(Resources.Load<GameObject>("Andrea"), 
									new Vector3(GameObject.Find("enemy2").transform.position.x, 
												GameObject.Find("enemy2").transform.position.y, 0.0f),
									Quaternion.identity);
                        enemyHP[1] -= enemyDamage;						
					}
					else{
						timerOn = true;
					}

                    isPlayerTurn = false;
                }
            }
        }

        //enemy turn
        if(enemyTurn)
        {
            for (int i = 0; i < enemyCount; ++i)
            {
                //enemy's dice roll
                GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                roll2 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
                //Debug.Log("enemy " + (i + 1) + ":" + roll2);

                //this is the players dice roll
                GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                roll1 = GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult;
                //Debug.Log("player: " + roll1);

                //checks if enemy hits or not
                if (roll2 >= roll1){
					// hero got hit, play damage indicator
					GameObject obj = Resources.Load<GameObject>("HeroDamageAnimator");					
					obj.GetComponent<HeroDamageAnimator>().heroDamage = heroDamage;
					Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

					Stats.health -= heroDamage;
				}

            }
            isPlayerTurn = true;
			enemyTurn = false;
        }

        //use item, end turn
        if (GUI.Button(whiskeyRect, "Whiskey"))
        {
            //heals the player
            if (Stats.whiskey > 0)
            {
                if (Stats.health < 100)
                {
                    if (Stats.health > 90)
                        Stats.health = 100;
                    else
                        Stats.health += WhiskeyHP;

					GameObject obj = Resources.Load<GameObject>("WhiskeyHPAnimator");					
					obj.GetComponent<WhiskeyHPAnimator>().healthPoint = WhiskeyHP;
					Instantiate(obj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

                    Stats.whiskey--;
                }

                isPlayerTurn = false;
            }
        }
        
        //run button
        if (GUI.Button(runRect1, "Run"))
        {
            if (isPlayerTurn)
            {
                GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().Roll();
                if (GameObject.FindGameObjectWithTag("Roll").GetComponent<HitRoll>().RollResult == 10)
                {
                    //Debug.Log("run");  // end battle player get's nothing and  enemy is destroyed.  
                    Application.LoadLevel("testScene3");
                }               
                isPlayerTurn = false;
            }
        }
    }


}
