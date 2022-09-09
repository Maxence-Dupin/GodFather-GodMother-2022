using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SecretDoor : MonoBehaviour
{
    private PlayerControls player;
    [SerializeField] private bool isInTrigger = false;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool phase1 = false;
    [SerializeField] private Text interactText;
    [SerializeField] private GameObject mesh;
    [SerializeField] private Transform firstPosition;
    [SerializeField] private Transform secondPosition;
    [SerializeField] private GameObject cadenas;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip clip;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isInTrigger && !isOpen && player.currentItem.itemName == "key")
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "Press E to open to open the secret door";
            if (Input.GetButtonDown("Interact"))
            {
                isOpen = true;
                interactText.gameObject.SetActive(false);
                StartCoroutine(LaunchPhase2());
                Destroy(cadenas);
                audiosource.PlayOneShot(clip);
            }
        }
        if (isOpen && !phase1)
        {
            mesh.transform.position = Vector3.Lerp(mesh.transform.position, firstPosition.position, Time.deltaTime);
        }
        else if (isOpen && phase1)
        {
            mesh.transform.position = Vector3.Lerp(mesh.transform.position, secondPosition.position, Time.deltaTime);
        }
    }

    public IEnumerator LaunchPhase2()
    {
        yield return new WaitForSeconds(1.5f);
        phase1 = true;
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