using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move to a treasure object in view and pick it up
public class Action_GetTreasure : Action
{
    private Treasure targetTreasure;

    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("hasTreasure", true));
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_DepositTreasure);
    }

    public override bool ValidAction()
    {
        //Valid if there's treasure visible and character isn't already carrying treasure
        if (perception.treasureInView.Count > 0 && character.carriedTreasure == null)
        {
            return true;
        }

        return false;
    }

    //Find the closest treasure out of those that are visible
    //Go to that treasure
    public override void ActionBegin()
    {
        targetTreasure = perception.FindClosestTarget(perception.treasureInView).GetComponent<Treasure>();
        character.SetDestination(targetTreasure.transform.position);
    }

    public override void ActionEnd()
    {
        targetTreasure = null;
    }

    //Recalculate closest treasure (tresure may have been picked up by another character, or a new treasure has com into view)
    //Go to that treasure
    public override void UpdateAction()
    {
        targetTreasure = perception.FindClosestTarget(perception.treasureInView).GetComponent<Treasure>();
        character.SetDestination(targetTreasure.transform.position);

        character.CheckDestination();
        if (character.reachedDestination)
        {
            if (character.carriedTreasure == null)
            {
                //Pick up the treasure upon reaching its location
                character.PickUpTreasure(perception.FindClosestTarget(perception.treasureInView).GetComponent<Treasure>());
            }
        }
    }
}
