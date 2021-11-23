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
