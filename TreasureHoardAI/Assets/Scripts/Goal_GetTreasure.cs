using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_GetTreasure : MonoBehaviour
{
    //Pick up treasure
    //Invalid if already holding treasure or if there is none nearby
    [SerializeField] bool nearTreasure;
    [SerializeField] bool carryingTreasure;
    [SerializeField] int priority = 20;

    public virtual int Prioritize()
    {
        return priority;
    }

    public virtual bool ValidGoal()
    {
        if (nearTreasure && !carryingTreasure)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
