using System.Collections;
using UnityEngine;

public class Tomb : MonoBehaviour
{
    private PlayerControls player;
    [SerializeField] private bool isInTrigger = false;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private GameObject shovel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isInTrigger && !isOpen)
        {
            if (Input.GetButtonDown("Interact"))
            {
                isOpen = true;
                animator.SetBool("IsOpen", true);
                StartCoroutine(StopAnimation());
                shovel.SetActive(true);
                audioSource.PlayOneShot(audioClip);
            }
        }
    }

    public IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(5f);
        animator.speed = 0;
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