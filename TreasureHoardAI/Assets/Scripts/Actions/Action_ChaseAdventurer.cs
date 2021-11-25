using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (perception.worldState.Contains(new KeyValuePair<string, object>("seesAdventurer", true)))
        {
            return true;
        }

        return false;
    }

    public override void ActionBegin()
    {
        //Move toward the nearest adventurer
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
