using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDownZone : MonoBehaviour
{
    public GameObject player;
    public float playerRotation = 0f;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Player")
        {
            player.transform.Rotate(playerRotation, 0f, 0f);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.name == "Player")
        {
            player.transform.Rotate(-playerRotation, 0f, 0f);
        }
    }
}
