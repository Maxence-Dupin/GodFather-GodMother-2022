using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public float exitRotation = 0;
    public GameObject hideText;
    public GameObject exitText;
    public GameObject player;
    public GameObject hideCamera;
    public bool hideCondition;
    public bool exitConditon;
    private void Start()
    {
        hideText.SetActive(false);
        exitText.SetActive(false);
        hideCondition = false;
        hideCamera.SetActive(false);
        exitConditon = false;
    }
    private void Update()
    {
        if(hideCondition == true)
        {

            if (Input.GetKeyDown(KeyCode.E) && exitConditon == false)
            {
                player.SetActive(false);
                hideCamera.SetActive(true);
                hideText.SetActive(false);
                exitText.SetActive(true);
                exitConditon = true;
            } else if (Input.GetKeyDown(KeyCode.E) && exitConditon == true)
            {
                player.SetActive(true);
                hideCamera.SetActive(false);
                hideText.SetActive(true);
                exitText.SetActive(false);
                exitConditon = false;
                player.transform.Rotate(0f, exitRotation, 0f);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.name == "Player")
        {
            hideText.SetActive(true);
            hideCondition = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.name == "Player")
        {
            hideText.SetActive(false);
            hideCondition = false;
        }
    }
}

