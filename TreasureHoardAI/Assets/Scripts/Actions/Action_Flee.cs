using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Turn around and run away after seeing a threat
//Since turning around means that the character can't see the threat any more,
//they flee a set distance before returning to normal behavior
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

    //Turn around and run away to a set location behind the character
    public override void ActionBegin()
    {
        character.SetDestination(transform.position - (transform.forward * 20));
    }

    public override void ActionEnd()
    {

    }

    //The character would normally stop seeing a dragon because they've turned around, but they need to see a dragon to flee from it
    //Change the priority of the flee goal to zero only after reaching flee destination
    public override void UpdateAction()
    {
        character.CheckDestination();
        if (character.reachedDestination)
        {
            GetComponent<Goal_Flee>().priority = 0;
        }
    }
}
