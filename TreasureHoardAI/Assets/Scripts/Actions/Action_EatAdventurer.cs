using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_EatAdventurer : Action
{
    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("eatenAdventurer", true));
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_EatAdventurer);
    }

    public override bool ValidAction()
    {
        if (perception.worldState.Contains(new KeyValuePair<string, object>("nearAdventurer", true)))
        {
            return true;
        }

        return false;
    }

    public override void ActionBegin()
    {
        //Eat the closest adventurer, as they've already been confirmed to be within eating range
        character.Eat(perception.FindClosestTarget(perception.adventurersInView).GetComponent<Character>());
    }

    public override void ActionEnd()
    {

    }

    public override void UpdateAction()
    {

    }
}
