using UnityEngine;
using System.Collections;

public class repeatingRandom : MonoBehaviour {
   
    void Start()
    {
        InvokeRepeating("PlaySound", 0.001f, 10f);
    }

    void PlaySound()
    {
        audio.Play();
    }

}
