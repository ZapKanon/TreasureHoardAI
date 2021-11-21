using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoal
{
    int Prioritize();
    bool ValidGoal();
    void UpdateGoal();
    void GoalBegin();
    void GoalEnd();
}

public class Goal : MonoBehaviour, IGoal
{
    protected Character character;
    protected PerceptionSystem perception;

    void Awake()
    {
        character = GetComponent<Character>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual int Prioritize()
    {
        return -1;
    }

    public virtual bool ValidGoal()
    {
        return false;
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
