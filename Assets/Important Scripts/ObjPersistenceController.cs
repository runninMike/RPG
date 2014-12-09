using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjPersistenceController : MonoBehaviour{
   public static Dictionary<string, Vector2> objTranspositionData;

   void Start(){
		objTranspositionData = new Dictionary<string, Vector2>();
		DontDestroyOnLoad (gameObject);
   }
   
    void Update(){
    }
}
