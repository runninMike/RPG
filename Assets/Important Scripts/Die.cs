using UnityEngine;
using System.Collections;

public abstract class Die: MonoBehaviour{
	
	protected int sides;

    public int RollResult{
        get;
        private set;
    }

    public void Roll(){
        Random.seed = System.DateTime.UtcNow.Millisecond;
        RollResult = Random.Range(0, sides) + 1;
        //Debug.Log(RollResult);
    }
}
