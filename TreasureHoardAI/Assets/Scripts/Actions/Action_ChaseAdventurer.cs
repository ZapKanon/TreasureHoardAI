using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move to the location of an adventurer
public class Action_ChaseAdventurer : Action
{
    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("nearAdventurer", true));
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_EatAdventurer);
    }

    public override bool ValidAction()
    {
        //Can only chase an adventurer if there's an adventurer to chase
        if (perception.worldState.Contains(new KeyValuePair<string, object>("seesAdventurer", true)))
        {
            return true;
        }

        return false;
    }

    //Move toward the nearest adventurer
    public override void ActionBegin()
    {      
        character.SetDestination(perception.FindClosestTarget(perception.adventurersInView).transform.position);
    }

    public override void ActionEnd()
    {

    }

    public override void UpdateAction()
    {
        character.CheckDestination();

        //Move toward the closest adventurer's new position
        if (!character.reachedDestination)
        {
            character.SetDestination(perception.FindClosestTarget(perception.adventurersInView).transform.position);
        }
    }
}
