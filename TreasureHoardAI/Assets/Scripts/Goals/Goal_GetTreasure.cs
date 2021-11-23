using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_GetTreasure : Goal
{
    //Pick up treasure
    [SerializeField] int priority = 20;

    public override int Prioritize()
    {
        return priority;
    }

}
