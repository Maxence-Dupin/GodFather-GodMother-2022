using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleItem : MonoBehaviour
{

    [SerializeField] private Item item;

    private void OnTriggerEnter(Collider other)
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.inventory.Add(item);
        player.currentItem = item;
        Destroy(this.gameObject);
    }
}
