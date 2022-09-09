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
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isInTrigger && player.currentItem.itemName == "shovel")
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "Press E to dig";
            if (Input.GetButtonDown("Interact"))
            {
                interactText.gameObject.SetActive(false);
                audioSource.PlayOneShot(audioClip);
                key.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
            interactText.gameObject.SetActive(false);
        }
    }
}