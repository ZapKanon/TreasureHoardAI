using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_DepositTreasure : Goal
{
    //Pick up nearby treasure, take it to the deposit point, and deposit it
    //Very high priority, but only valid if carrying treasure
    [SerializeField] int priority = 30;

    public override void Awake()
    {
        base.Awake();
        preconditions.Add(new KeyValuePair<string, object>("depositedTreasure", true));
        preconditions.Add(new KeyValuePair<string, object>("atDepositPoint", true));
        preconditions.Add(new KeyValuePair<string, object>("hasTreasure", true));
    }

    public override int Prioritize()
    {
        return priority;
    }
}
