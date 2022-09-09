using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioSource soundSource2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            soundSource.Play();
            soundSource2.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
