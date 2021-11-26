using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Flee : Action
{
    Vector3 fleePoint;

    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("fleeing", true));
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_Flee);
    }

    public override bool ValidAction()
    {
        return true;
    }

    //Turn around and run away
    public override void ActionBegin()
    {
        character.SetDestination(transform.position - (transform.forward * 20));
    }

    public override void ActionEnd()
    {

    }

    //The character would normally stop seeing a dragon because they've turned around, but they need to see a dragon to flee from it
    public override void UpdateAction()
    {
        character.CheckDestination();
        if (character.reachedDestination)
        {
            GetComponent<Goal_Flee>().priority = 0;
        }
    }
}
