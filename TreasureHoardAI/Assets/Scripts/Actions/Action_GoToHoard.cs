using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Go to the dragon's hoard, since that's where treasure is likely to be
//Characters know where the hoard is at all times
public class Action_GoToHoard : Action
{
    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("atHoard", true));
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_GoToHoard);
    }

    public override bool ValidAction()
    {
        return true;
    }

    public override void ActionBegin()
    {
        character.SetDestination(perception.locationsManager.hoardPoint.position);
    }

    public override void ActionEnd()
    {

    }

    //If there's no treasure left at the hoard, characters may just stand there
    public override void UpdateAction()
    {
        character.CheckDestination();
        if(character.reachedDestination)
        {
            Debug.Log("At hoard. Nothing to do...");
        }
    }
}
