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

    //Invalid if already holding treasure or if there is none nearby
    public override bool ValidGoal()
    {
        if (perception.treasureInView.Count > 0 && !perception.carryingTreasure)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
