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
    public EnemyControllerAi enemyControllerAi;
    public MeshRenderer meshPlayer;
    public CharacterController characterController;
    public GameObject enemy;
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
                meshPlayer.enabled = false;
                enemyControllerAi.viewRadius = 0;
                enemyControllerAi.nearViewRadius = 0;
                characterController.enabled = false;
                hideCamera.SetActive(true);
                hideText.SetActive(false);
                exitText.SetActive(true);
                exitConditon = true;
                if (enemy.transform.position == gameObject.transform.position)
                {
                    
                }
                
                
            } else if (Input.GetKeyDown(KeyCode.E) && exitConditon == true)
            {
                meshPlayer.enabled = true;
                enemyControllerAi.viewRadius = 8;
                enemyControllerAi.nearViewRadius = 3;
                characterController.enabled = true;
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
        StartCoroutine(TimeToStayOnHide());
        IEnumerator TimeToStayOnHide()
            {
            if (collider.name == "Enemy")
            {
                yield return new WaitForSeconds(7);
                enemyControllerAi.canSee = false;
            }
        }
       
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

