using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Walk to preset points surrounding the dragon's hoard in a clockwise fashion
//Characters know where these points are at all times
public class Action_Patrol : Action
{
    protected override void Start()
    {
        base.Start();
        resultEffects.Add(new KeyValuePair<string, object>("patrolling", true));
    }

    public override System.Type GetGoal()
    {
        return typeof(Goal_Patrol);
    }

    public override bool ValidAction()
    {
        return true;
    }

    public override void ActionBegin()
    {
        //Start patrolling at a random point
        character.SetDestination(perception.locationsManager.dragonPatrolPoints[character.currentPatrolPoint = Random.Range(0, perception.locationsManager.dragonPatrolPoints.Count)].position);
    }

    public override void ActionEnd()
    {

    }

    //Check if the destionation point has been reached
    //If it has, set destination to the next patrol point in the list
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
