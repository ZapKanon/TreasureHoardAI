using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Patrol : Action
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_Patrol);
    }

    public override void ActionBegin()
    {
        //Start patrolling at a random point
        character.SetDestination(perception.locationsManager.dragonPatrolPoints[character.currentPatrolPoint = Random.Range(0, perception.locationsManager.dragonPatrolPoints.Count)].position);
    }

    public override void ActionEnd()
    {

    }

    public override void UpdateAction()
    {
        character.CheckDestination();

        if (character.reachedDestination)
        {
            //Update current patrol point
            character.currentPatrolPoint++;

            if(character.currentPatrolPoint >= perception.locationsManager.dragonPatrolPoints.Count)
            {
                character.currentPatrolPoint = 0;
            }

            //Move to next patrol point
            character.SetDestination(perception.locationsManager.dragonPatrolPoints[character.currentPatrolPoint].position);
        }
    }
}
