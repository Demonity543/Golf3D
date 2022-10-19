using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObj : MonoBehaviour
{

    public AudioSource Hit;
    public AudioSource End;
    public AudioSource Reset;

    public void playHit()
    {
        Hit.Play();
    }
    public void playEnd()
    {
        Reset.Play();
    }
    public void playReset()
    {
        Reset.Play();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
