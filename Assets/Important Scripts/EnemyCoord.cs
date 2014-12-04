using UnityEngine;
using System.Collections;

public class EnemyCoord : MonoBehaviour {

    public float x;
    public float y;

    void Start()
    {
        transform.position = new Vector3(x, y, 0.0f);
    }

}
