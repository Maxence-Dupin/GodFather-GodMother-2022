using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hole : MonoBehaviour
{

    private PlayerControls player;
    [SerializeField] private bool isInTrigger = false;
    [SerializeField] private Text interactText;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInTrigger && player.currentItem.itemName == "shovel")
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "Press E to dig";
            if(Input.GetKeyDown(KeyCode.E))
            {
                interactText.gameObject.SetActive(false);
                audioSource.PlayOneShot(audioClip);
                key.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
        interactText.gameObject.SetActive(false);
    }
}
