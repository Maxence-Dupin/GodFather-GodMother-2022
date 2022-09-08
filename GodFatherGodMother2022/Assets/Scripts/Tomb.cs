using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.UI;

public class Tomb : MonoBehaviour
{

    private PlayerControls player;
    [SerializeField] private bool isInTrigger = false;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private Text interactText;
    [SerializeField] private GameObject shovel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && !isOpen)
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "Press E to open the tomb";
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = true;
                animator.SetBool("IsOpen", true);
                StartCoroutine(StopAnimation());
                interactText.gameObject.SetActive(false);
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
        interactText.gameObject.SetActive(false);
    }
}
