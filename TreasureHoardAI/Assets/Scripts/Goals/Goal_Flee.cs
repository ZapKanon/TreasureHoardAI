using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Flee : Goal
{
    //Patrol around the dragon's hoard
    //By default a low priority goal that activates when there's nothing else to do
    [SerializeField] public int priority = 0;

    public override void Awake()
    {
        base.Awake();
        preconditions.Add(new KeyValuePair<string, object>("fleeing", true));
    }

    public override int Prioritize()
    {
        if (perception.worldState.Contains(new KeyValuePair<string, object>("seesDragon", true)))
        {
            priority = 30;
        }

        return priority;
    }
}
