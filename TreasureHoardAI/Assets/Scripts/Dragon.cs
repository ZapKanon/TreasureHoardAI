using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Character
{
    private int currentPatrolPoint = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        PatrolToNextPoint();
    }

    /// <summary>
    /// The dragon patrols in a square around its hoard when not chasing adventurers
    /// </summary>
    private void PatrolToNextPoint()
    {
        //Set destination to current patrol point
        navMeshAgent.destination = dragonPatrolPoints[currentPatrolPoint].position;

        //Update current patrol point after arriving
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < arrivalDistance)
        {
            if (currentPatrolPoint >= dragonPatrolPoints.Count - 1)
            {
                currentPatrolPoint = 0;
            }
            else
            {
                currentPatrolPoint++;
            }
        }
    }
}
