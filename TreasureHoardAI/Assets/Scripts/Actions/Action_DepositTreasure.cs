using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_DepositTreasure : Action
{
    // Start is called before the first frame update
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
        if (character.carriedTreasure != null && perception.worldState.Contains(new KeyValuePair<string, object>("atDepositPoint", true)))
        {
            return true;
        }

        return false;
    }

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
