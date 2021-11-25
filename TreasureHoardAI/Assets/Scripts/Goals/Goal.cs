using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoal
{
    int Prioritize();
    Action ValidGoal(Action[] actions);
    void UpdateGoal();
    void GoalBegin();
    void GoalEnd();
}

public class Goal : MonoBehaviour, IGoal
{
    protected Character character;
    protected PerceptionSystem perception;
    public List<KeyValuePair<string, object>> preconditions;

    public virtual void Awake()
    {
        character = GetComponent<Character>();
        perception = GetComponent<PerceptionSystem>();
        preconditions = new List<KeyValuePair<string, object>>();
    }

    void Update()
    {
        UpdateGoal();
    }

    public virtual int Prioritize()
    {
        return -1;
    }

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

            if (candidateAction != null)
            {
                preconditionsCopy.RemoveAt(0);

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

    public virtual void UpdateGoal()
    {

    }

    public virtual void GoalBegin()
    {

    }

    public virtual void GoalEnd()
    {

    }
}
