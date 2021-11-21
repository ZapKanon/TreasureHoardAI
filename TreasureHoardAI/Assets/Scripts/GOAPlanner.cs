using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Determine highest priority goal and corresponding action
        Goal optimalGoal = null;
        Action optimalAction = null;

        foreach (Goal goal in goals)
        {
            //Let each goal get its priority in order
            goal.UpdateGoal();

            //If the goal is currently invalid, skip over it
            if (!goal.ValidGoal())
            {
                continue;
            }

            //If the goal has a lower priority than the current optimal goal, skip over it
            if (!(optimalGoal == null || goal.Prioritize() > optimalGoal.Prioritize()))
            {
                continue;
            }

            Action newAction = null;
            foreach(Action action in actions)
            {
                //If the goal doesn't correspond to any available action, skip over it
                if (action.GetGoal() != goal.GetType())
                {
                    continue;
                }

                //Set candidate action
                newAction = action;
            }

            //If an action remains after these checks, update optimalGoal
            if (newAction != null)
            {
                optimalGoal = goal;
                optimalAction = newAction;
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
        //Same goal as before
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
        //Setting a new goal
        else if (activeGoal != optimalGoal)
        {
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

        //Update the action
        if (activeAction != null)
        {
            activeAction.UpdateAction();
        }

    }
}
