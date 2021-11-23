using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    Character carryingCharacter = null;
    private bool pickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Called when treasure is picked up by a character
    //Disables the treasure's renderer and sets it to follow the character's position
    public void PickedUp(Character character)
    {
        carryingCharacter = character;
        carryingCharacter.carriedTreasure = this;
        gameObject.SetActive(false);
    }

    //Called when a character drops their treasure
    //Enables the treasure's renderer and places it at the character's position
    public void Deposited(Character character)
    {
        carryingCharacter = null;
        character.carriedTreasure = null;
        transform.position = new Vector3(carryingCharacter.transform.position.x, transform.position.y, carryingCharacter.transform.position.z);
    }
}