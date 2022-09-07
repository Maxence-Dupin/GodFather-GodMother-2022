using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public PlayerMovement playerMovement;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.name == "Player")
        {
            playerMovement.speed *= 0.5f;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.name == "Player")
        {
            playerMovement.speed *= 2f;
        }
    }
}
