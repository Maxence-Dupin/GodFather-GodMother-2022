using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int speed;
    [SerializeField] private int rotationSpeed;

    public List<Item> inventory;
    public Item currentItem;

    // Update is called once per frame
    void Update()
    {
        //déplacement du perso
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, translation);
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        //changer d'objet en main
        if (Input.GetKeyDown("space") && inventory.Count > 1)
        {
            if(inventory.IndexOf(currentItem) + 1 < inventory.Count)
            {
                currentItem = inventory[inventory.IndexOf(currentItem) + 1];
            }
            else
            {
                currentItem = inventory[0];
            }
        }
    }
}
