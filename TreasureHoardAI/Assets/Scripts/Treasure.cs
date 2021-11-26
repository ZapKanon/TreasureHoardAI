using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Treasure objects are stationary. They sit in the dragon's hoard until an adventurer picks them up.
//Taking a piece of treasure to a deposit location scores a point for that adventurer's team.
public class Treasure : MonoBehaviour
{
    Character carryingCharacter = null;
    public bool beingCarried = false;

    //Called when treasure is picked up by a character
    //Disables the treasure as long as it is being carried
    public void PickedUp(Character character)
    {
        if (beingCarried == false)
        {
            beingCarried = true;
            carryingCharacter = character;
            carryingCharacter.carriedTreasure = this;
            gameObject.SetActive(false);
        }
    }

    //Called when a character drops their treasure, either at a deposit point or on the ground
    //Enables the treasure's renderer and places it at the character's position
    public void Deposited(Character character)
    {
        carryingCharacter = null;
        character.carriedTreasure = null;
        //Move the previously invisible treasure to where the character is
        transform.position = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
        beingCarried = false;

        Debug.Log("Treasure Deposited!");
    }
}
