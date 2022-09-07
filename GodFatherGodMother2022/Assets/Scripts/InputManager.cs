using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Player player;
    private Player.PS1Actions ps1;

    private PlayerMovement playerMovement;
    void Awake()
    {
        player = new Player();
        ps1 = player.PS1;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        playerMovement.DoMovement(ps1.Movement.ReadValue<Vector2>());   
    }

    private void OnEnable()
    {
        ps1.Enable();
    }

    private void OnDisable()
    {
        ps1.Disable();
    }
}
