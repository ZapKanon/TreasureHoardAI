using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Goal Oriented Action Planner processes a character's list of goals against their list of actions
//If the available actions + the current world state allow for a goal to be completed,
//begin executing actions that lead to that goal.
public class GOAPlanner : MonoBehaviour
{
    Goal[] goals;
    Action[] actions;

    [SerializeField] Goal activeGoal;
    [SerializeField] Action activeAction;

    void Awake()
    {
        goals = GetComponents<Goal>();
        actions = GetComponents<Action>();
    }

    // Check all goals, actions, and world state to determine what action to take
    void Update()
    {
        //Determine highest priority goal and corresponding action
        Goal optimalGoal = null;
        Action optimalAction = null;

        foreach (Goal goal in goals)
        {
            //Let each goal get its priority figured out
            goal.UpdateGoal();

            //If the goal is currently invalid, skip over it
            Action candidateAction = goal.ValidGoal(actions);
            if (candidateAction == null || !candidateAction.ValidAction())
            {
                continue;
            }

            //If the goal has a lower priority than the current optimal goal, skip over it
            if (!(optimalGoal == null || goal.Prioritize() > optimalGoal.Prioritize()))
            {
                continue;
            }

            //If an action remains after these checks, update optimalGoal
            if (candidateAction != null)
            {
                optimalGoal = goal;
                optimalAction = candidateAction;
            }
        }

        //If character has no current goal
        if (activeGoal == null)
        {
            activeGoal = optimalGoal;
            activeAction = optimalAction;

            //Perform goal / action activation logic
            if (activeGoal != null)
            {
                activeGoal.GoalBegin();
            }
            if (activeAction != null)
            {
                activeAction.ActionBegin();
            }
        }
        //If the goal is the same goal as before
        else if (activeGoal == optimalGoal)
        {
            //Action could change while achieving same goal
            if (activeAction != optimalAction)
            {
                activeAction.ActionEnd();
                activeAction = optimalAction;
                activeAction.ActionBegin();
            }
        }
        //If a new goal is being set as optimal
        else if (activeGoal != optimalGoal)
        {
            //End the previous goal and action
            activeGoal.GoalEnd();
            activeAction.ActionEnd();

            activeGoal = optimalGoal;
            activeAction = optimalAction;

            //Perform goal / action activation logic
            if(activeGoal != null)
            {
                activeGoal.GoalBegin();
            }
            if (activeAction != null)
            {
                activeAction.ActionBegin();
            }
        }

        //Update the action each frame
        if (activeAction != null)
        {
            activeAction.UpdateAction();
        }

    }
}
