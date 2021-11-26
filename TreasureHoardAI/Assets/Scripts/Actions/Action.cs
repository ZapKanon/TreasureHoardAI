using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Actions are character behaviors that putput certain result effects when completed
//These result effects are used to fulfill goal preconditions
//Actions explicitly state which goals they are able to aid in fulfilling to make the ValidGoal process simpler
//They also have costs to make decisions between multiple actions that aid in completing the same goal precondition
public class Action : MonoBehaviour
{
    protected Character character;
    protected PerceptionSystem perception;

    public List<KeyValuePair<string, object>> resultEffects;
    public int cost;

    //Initialize and get component references
    protected virtual void Start()
    {
        character = GetComponent<Character>();
        perception = GetComponent<PerceptionSystem>();
        resultEffects = new List<KeyValuePair<string, object>>();
        cost = 1;
    }

    //Return the goal that this action helps to complete
    public virtual System.Type GetGoal()
    {
        return null;
    }

    //Return whether this action can be completed based on current world state
    public virtual bool ValidAction()
    {
        return false;
    }

    //Anything that occurs immediately when the action is chosen
    public virtual void ActionBegin()
    {

    }

    //Anything that occurs when the action is dropped in favor of another
    public virtual void ActionEnd()
    {

    }

    //Anything that needs to occur constantly while the action is active
    public virtual void UpdateAction()
    {

    }
}
