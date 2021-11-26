using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_EatAdventurer : Goal
{
    //Chase an adventurer and devour them if close enough
    //High priority, but only possible if an adventurer is in view
    [SerializeField] int priority = 20;

    public override void Awake()
    {
        base.Awake();
        preconditions.Add(new KeyValuePair<string, object>("eatenAdventurer", true));
        preconditions.Add(new KeyValuePair<string, object>("nearAdventurer", true));
        preconditions.Add(new KeyValuePair<string, object>("seesAdventurer", true));
    }

    public override int Prioritize()
    {
        return priority;
    }
}
