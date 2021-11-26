using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Deposit treasure at a deposit location
public class Action_DepositTreasure : Action
{
    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("depositedTreasure", true));
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_DepositTreasure);
    }

    public override bool ValidAction()
    {
        //Can only deposit treasure if within range of a deposit location
        if (character.carriedTreasure != null && perception.worldState.Contains(new KeyValuePair<string, object>("atDepositPoint", true)))
        {
            return true;
        }

        return false;
    }

    //Character immediately deposits their treasure
    public override void ActionBegin()
    {
        character.DepositTreasure();      
    }

    public override void ActionEnd()
    {
        
    }

    public override void UpdateAction()
    {

    }
}
