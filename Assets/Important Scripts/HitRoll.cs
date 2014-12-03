using UnityEngine;
using System.Collections;

public class HitRoll : MonoBehaviour {

    public int RollResult
    {
        get;
        private set;
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Roll();
	}

    public void Roll()
    {
        Random.seed = System.DateTime.UtcNow.Millisecond;
        RollResult = Random.Range(0, 6) + 1;
        //Debug.Log(RollResult);
    }
    
}
