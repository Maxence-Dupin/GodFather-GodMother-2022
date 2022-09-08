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

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }

    public void DoMovement(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.z = input.y;
        //transform.Translate(0f, 0f, speed * Input.GetAxis("Vertical") * Time.deltaT ime);
        characterController.Move(transform.TransformDirection(new Vector3(0,0, Input.GetAxis("Vertical") )) * speed * Time.deltaTime);
        transform.Rotate(0f, speedRotation * Input.GetAxis("Horizontal") * Time.deltaTime, 0f);

        headBob.SetTrigger("Walk");
    }
}
