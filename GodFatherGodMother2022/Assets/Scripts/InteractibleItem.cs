using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleItem : MonoBehaviour
{

    [SerializeField] private Item item;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shovelClip;
    [SerializeField] private AudioClip keyClip;
    [SerializeField] private AudioClip treasureClip;

    private void OnTriggerEnter(Collider other)
    {
        PlayerControls player = GameObject.FindWithTag("Player").GetComponent<PlayerControls>();
        player.inventory.Add(item);
        player.currentItem = item;
        if(item.itemName == "shovel")
        {
            audioSource.PlayOneShot(shovelClip);
        }
        else if (item.itemName == "key")
        {
            audioSource.PlayOneShot(keyClip);
        }
        else if (item.itemName == "treasure")
        {
            audioSource.PlayOneShot(treasureClip);
        }
        Destroy(this.gameObject);
    }
}
