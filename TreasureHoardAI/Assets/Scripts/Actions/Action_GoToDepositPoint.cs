using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GoToDepositPoint : Action
{
    // Start is called before the first frame update
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
        if (character.carriedTreasure != null)
        {
            return true;
        }

        return false;
    }

    public override void ActionBegin()
    {
        character.SetDestination(perception.locationsManager.depositPoints[(int)character.team].position);
    }

    public override void ActionEnd()
    {
        
    }

    public override void UpdateAction()
    {
        character.CheckDestination();
        if (character.reachedDestination)
        {
            
        }
    }
}
