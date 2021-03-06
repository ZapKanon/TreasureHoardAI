using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Patrol : Goal
{
    //Patrol around the dragon's hoard
    //By default a low priority goal that activates when there's nothing else to do
    [SerializeField] int priority = 10;

    //Set up preconditions in REVERSE order
    public override void Awake()
    {
        base.Awake();
        preconditions.Add(new KeyValuePair<string, object>("patrolling", true));
    }

    public override int Prioritize()
    {
        return priority;
    }
}
