using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int sprintSpeed;
    [SerializeField] private int rotationSpeed;

    public List<Item> inventory;
    public Item currentItem;

    public bool canSprint;
    [SerializeField] private float stamina = 100;
    [SerializeField] private float staminaLossPerSecond;
    [SerializeField] private float staminaGainPerSecond;
    [SerializeField] private float staminaRegainTimer = 4;

    // Update is called once per frame
    private void Update()
    {
#if UNITY_EDITOR
        TestInputs();
#endif

        staminaRegainTimer += Time.deltaTime;
        //déplacement du perso
        if (Input.GetButton("Sprint") && stamina > 0 && staminaRegainTimer > 4)
        {
            canSprint = true;
        }
        else
        {
            canSprint = false;
        }

        if (canSprint && Input.GetAxis("Vertical") != 0)
        {
            float translation = Input.GetAxis("Vertical") * sprintSpeed * Time.deltaTime;
            transform.Translate(0, 0, translation);
            stamina -= staminaLossPerSecond * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, 100);
            if (stamina == 0)
            {
                canSprint = false;
                staminaRegainTimer = 0;
            }
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(0, 0, translation);
        }

        if (!canSprint && staminaRegainTimer > 2)
        {
            stamina += staminaGainPerSecond * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, 100);
        }

        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        //changer d'objet en main
        if (Input.GetButtonDown("Hand") && inventory.Count > 1)
        {
            if (inventory.IndexOf(currentItem) + 1 < inventory.Count)
            {
                currentItem = inventory[inventory.IndexOf(currentItem) + 1];
            }
            else
            {
                currentItem = inventory[0];
            }
        }
    }

#if UNITY_EDITOR
    private void TestInputs()
    {
        if (!Input.anyKey)
        {
            Debug.LogError("None");
            return;
        }

        if (Input.GetButton("Sprint"))
            Debug.LogError("Sprint");
        if (Input.GetButton("Interact"))
            Debug.LogError("Interact");
        if (Input.GetButton("Hand"))
            Debug.LogError("Hand");
        if (Input.GetButton("Skip"))
            Debug.LogError("Skip");
    }
#endif
}