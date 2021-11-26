using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Go to the deposit location that matches the character's team
//Characters know where their deposit location is at all times
public class Action_GoToDepositPoint : Action
{
    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("atDepositPoint", true));
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_DepositTreasure);
    }

    public override bool ValidAction()
    {
        //Only go to the deposit point if carrying treasure
        if (character.carriedTreasure != null)
        {
            return true;
        }

        return false;
    }

    //Move to the deposit location
    public override void ActionBegin()
    {
        character.SetDestination(perception.locationsManager.depositPoints[(int)character.team].position);
    }

    public override void ActionEnd()
    {
        
    }

    public override void UpdateAction()
    {

    }
}
