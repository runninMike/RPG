using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour 
{
    public static int health = 100;
    public static int whiskey = 4;
    public static int exp = 1000;
    public static int level = 0;

    void Start()
    {
       GameObject.DontDestroyOnLoad(GameObject.FindGameObjectWithTag("GlobalObj"));

    }

    void Update()
    {
        //very simple leveling mechanic
        if (exp > level * 1000 && level > 0)
        {
            level++;
            health = 100 + (level * 10);
        }

        //cap for health
        if (health > 100)
            health = 100 + (level * 10);
    }
}
