using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    protected Character character;
    protected PerceptionSystem perception;
    //protected Goal goal;

    public List<KeyValuePair<string, object>> resultEffects;
    public int cost;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        character = GetComponent<Character>();
        perception = GetComponent<PerceptionSystem>();
        resultEffects = new List<KeyValuePair<string, object>>();
        cost = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual System.Type GetGoal()
    {
        return null;
    }

    public virtual bool ValidAction()
    {
        return false;
    }


    public virtual void ActionBegin()
    {

    }

    public virtual void ActionEnd()
    {

    }

    public virtual void UpdateAction()
    {

    }
}
