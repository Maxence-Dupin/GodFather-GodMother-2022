using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource soundSource;
    private void OnTriggerEnter(Collider other)
    {
        soundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
