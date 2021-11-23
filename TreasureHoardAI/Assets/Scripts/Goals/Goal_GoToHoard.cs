using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_GoToHoard : Goal
{
    //Go to the location of the dragon's hoard
    //By default a low priority goal that activates when there's nothing else to do
    [SerializeField] int priority = 10;

    public override void Awake()
    {
        base.Awake();
        preconditions.Add(new KeyValuePair<string, object>("atHoard", true));
        Debug.Log(preconditions.Count);
    }

    public override int Prioritize()
    {
        return priority;
    }
}
