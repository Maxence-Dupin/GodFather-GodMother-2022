using UnityEngine;

public class Hole : MonoBehaviour
{
    private PlayerControls player;
    [SerializeField] private bool isInTrigger = false;

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
            if (Input.GetButtonDown("Interact"))
            {
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
    }
}