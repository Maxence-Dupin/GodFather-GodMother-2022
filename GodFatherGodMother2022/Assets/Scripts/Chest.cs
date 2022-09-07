using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{

    private PlayerControls player;
    [SerializeField] private bool isInTrigger = false;
    [SerializeField] private Text interactText;
    [SerializeField] private GameObject treasure;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && player.currentItem.itemName == "key")
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "Press E to open the chest";
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactText.gameObject.SetActive(false);
                treasure.SetActive(true);
                Destroy(this.gameObject);
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
