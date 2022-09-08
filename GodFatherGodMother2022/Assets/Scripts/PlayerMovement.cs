using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    public float speed = 2f;
    public float sprintSpeed = 5f;
    public float speedRotation = 3f;
    public PlayerControls playerControls;

    public Animator headBob;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerControls = GetComponent<PlayerControls>();
    }

    void Update()
    {
        
    }

    public void DoMovement(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.z = input.y;
        if(playerControls.canSprint)
        {
            characterController.Move(transform.TransformDirection(moveDirection) * sprintSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        }
        transform.Rotate(0f, speedRotation * Input.GetAxis("Horizontal") * Time.deltaTime, 0f);

        headBob.SetTrigger("Walk");
    }
}
