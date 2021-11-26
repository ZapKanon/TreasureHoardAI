using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Flee : Goal
{
    //Run away from a dragon
    //By default a zero priority goal that activates when a dragon is in view
    [SerializeField] public int priority = 0;

    //Set up preconditions in REVERSE order
    public override void Awake()
    {
        base.Awake();
        preconditions.Add(new KeyValuePair<string, object>("fleeing", true));
    }

    //Increase priority to very high value if a dragon is visible
    //This is probably a suboptimal way to do this, but I was running into problems
    //The adventurer would see the dragon, turn around to flee, and stop seeing the dragon
    //This method keeps prioirty high until they've actually reached their flee destination
    public override int Prioritize()
    {
        if (perception.worldState.Contains(new KeyValuePair<string, object>("seesDragon", true)))
        {
            priority = 30;
        }

        return priority;
    }
}
