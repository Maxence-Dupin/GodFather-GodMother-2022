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

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerControls = GetComponent<PlayerControls>();
    }

    public void DoMovement()
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.z = Input.GetAxis("Vertical");
        if (playerControls.canSprint)
        {
            if (characterController.enabled) characterController.Move(sprintSpeed * Time.deltaTime * transform.TransformDirection(moveDirection));
        }
        else
        {
            if (characterController.enabled) characterController.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));
        }
        transform.Rotate(0f, speedRotation * Input.GetAxis("Horizontal") * Time.deltaTime, 0f);

        headBob.SetTrigger("Walk");
    }
}