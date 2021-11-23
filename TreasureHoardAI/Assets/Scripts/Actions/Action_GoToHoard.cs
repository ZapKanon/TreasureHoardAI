using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GoToHoard : Action
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("atHoard", true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_GoToHoard);
    }

    public override bool ValidAction()
    {
        return true;
    }

    public override void ActionBegin()
    {
        character.SetDestination(perception.locationsManager.hoardPoint.position);
    }

    public override void ActionEnd()
    {

    }

    public override void UpdateAction()
    {
        character.CheckDestination();
        if(character.reachedDestination)
        {
            Debug.Log("At hoard. Nothing to do...");
        }
    }
}
