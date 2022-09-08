using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        playerMovement.DoMovement();
    }
}