using System.Collections;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public float exitRotation = 0;
    public GameObject player;
    public GameObject hideCamera;
    public bool hideCondition;
    public bool exitConditon;
    public EnemyControllerAi enemyControllerAi;
    public MeshRenderer meshPlayer;
    public CharacterController characterController;
    public GameObject enemy;
    public GameObject playerCam;

    private void Start()
    {
        hideCondition = false;
        hideCamera.SetActive(false);
        exitConditon = false;
    }
    private void Update()
    {
        if (hideCondition == true)
        {
            if (Input.GetButtonDown("Interact") && exitConditon == false)
            {
                exitConditon = true;
                meshPlayer.enabled = false;
                enemyControllerAi.viewRadius = 0;
                enemyControllerAi.nearViewRadius = 0;
                characterController.enabled = false;
                hideCamera.SetActive(true);
                playerCam.SetActive(false);
            }
            else if (Input.GetButtonDown("Interact") && exitConditon == true)
            {
                exitConditon = false;
                meshPlayer.enabled = true;
                enemyControllerAi.viewRadius = 8;
                enemyControllerAi.nearViewRadius = 3;
                characterController.enabled = true;
                hideCamera.SetActive(false);
                playerCam.SetActive(true);

                player.transform.Rotate(0f, exitRotation, 0f);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)

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

        if (collider.name == "Player")
        {
            hideCondition = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.name == "Player")
        {
            hideCondition = false;
        }
    }
}