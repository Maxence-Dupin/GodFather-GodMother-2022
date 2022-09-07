using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    public float speed = 3f;
    public float speedRotation = 3f;

    public Animator headBob;

    /*public float headBob = 0f;
    public float headBob = 0f;*/

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }

    public void DoMovement(Vector2 input)
    {
        /*Vector3 rotatePlayer = Vector3.zero;*/
        Vector3 moveDirection = Vector3.zero;
        /*rotatePlayer.x = input.x;*/
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        transform.Rotate(0f, speedRotation * Input.GetAxis("Horizontal") * Time.deltaTime, 0f);

        headBob.SetTrigger("Walk");
    }
}
