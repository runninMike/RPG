using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjPersistenceController : MonoBehaviour{
   public static Dictionary<string, Vector3D> objTranspositionData;

   void Start(){
		objTranspositionData = new Dictionary<string, Vector3D>();
   }
   
    void Update(){
    }
}
