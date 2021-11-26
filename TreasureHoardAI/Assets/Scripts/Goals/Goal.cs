using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Goals are attached to characters. They consist of a collection of preconditions that must be fulfilled to complete the goal.
//These preconditions can be fulfilled through natural world state occurences or by completing actions.

//An interface of all required goal methods
public interface IGoal
{
    int Prioritize();
    Action ValidGoal(Action[] actions);
    void UpdateGoal();
    void GoalBegin();
    void GoalEnd();
}

//Base goal class
public class Goal : MonoBehaviour, IGoal
{
    protected Character character;
    protected PerceptionSystem perception;
    public List<KeyValuePair<string, object>> preconditions;

    //Initialize and get component references
    public virtual void Awake()
    {
        character = GetComponent<Character>();
        perception = GetComponent<PerceptionSystem>();
        preconditions = new List<KeyValuePair<string, object>>();
    }

    //Perform any necessary updates each frame
    void Update()
    {
        UpdateGoal();
    }

    //Default priority value lower than any real goal
    public virtual int Prioritize()
    {
        return -1;
    }

    //Use perception system world state information and available actions to find a route to complete the goal
    //Work backwards through the goal's preconditions to find a chain of actions that achieve the goal.
    //(This is the most important part of this GOAP implementation)
    public Action ValidGoal(Action[] actions)
    {
        //Make a copy of the goal's preconditions to test against
        List<KeyValuePair<string, object>> preconditionsCopy = new List<KeyValuePair<string, object>>(preconditions);

        //Check each precondition against the world state. They might already be fulfilled!
        foreach (KeyValuePair<string, object> precondition in preconditions)
        {
            foreach (KeyValuePair<string, object> state in perception.worldState)
            {
                //If the precondition's desired state is already the case in the world
                if (precondition.Key.ToString() == state.Key.ToString() && precondition.Value.ToString() == state.Value.ToString())
                {
                    //Remove the completed precondition
                    preconditionsCopy.Remove(precondition);
                }
            }
        }

        Action candidateAction = null;

        //Continue until all preconditions have been met
        while (preconditionsCopy.Count > 0)
        {
            //Check all actions to find one that meets the precondition
            foreach (Action action in actions)
            {
                //Make sure the action is relevant to the intended goal
                if (action.GetGoal() == GetType())
                {
                    //Check all of the action's result effects against the goal's precondition at index 0 (the last unfulfilled precondition)
                    foreach (KeyValuePair<string, object> effect in action.resultEffects)
                    {
                        //If an effect matches the precondition's desired state
                        if (effect.Key.ToString() == preconditionsCopy[0].Key.ToString() && effect.Value.ToString() == preconditionsCopy[0].Value.ToString())
                        {
                            //If this action has a lower cost than the previous action (or the previous action doesn't exist) make this action the candidate for execution
                            if ((candidateAction != null && action.cost < candidateAction.cost) || candidateAction == null)
                            {
                                candidateAction = action;
                            }
                        }
                    }
                }
            }

            //If a suitable action has been found, remove the precondition it was checking against
            if (candidateAction != null)
            {
                preconditionsCopy.RemoveAt(0);

                //IF there are no more preconditions, then they've all been fulfilled
                //The action that fulfilled the last precondition (the first that needs to be achieved) becomes a candidate action to be executed
                if (preconditionsCopy.Count == 0)
                {
                    return candidateAction;
                }
                else
                {
                    candidateAction = null;
                }
            }
            else
            {
                return null;
            }
        }

        return candidateAction;
    }

    //Anything that needs to be updated constantly
    public virtual void UpdateGoal()
    {

    }

    //Anything that occurs immediately when the goal is chosen
    public virtual void GoalBegin()
    {

    }

    //Anything that occurs when the goal is dropped in favor of another goal
    public virtual void GoalEnd()
    {

    }
}
