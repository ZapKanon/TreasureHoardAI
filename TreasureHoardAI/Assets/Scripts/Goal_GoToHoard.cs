using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_GoToHoard : MonoBehaviour
{
    //Go to the location of the dragon's hoard
    //By default a low priority goal that activates when there's nothing else to do
    [SerializeField] int priority = 10;
    public virtual int Prioritize()
    {
        return priority;
    }

    public virtual bool ValidGoal()
    {
        return true;
    }
}
