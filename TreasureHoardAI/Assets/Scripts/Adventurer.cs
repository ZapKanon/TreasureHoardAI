using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adventurer : Character
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GoToHoard();
    }

    /// <summary>
    /// The adventurer travels to the hoard in search of treasure.
    /// </summary>
    private void GoToHoard()
    {
        navMeshAgent.destination = hoardLocation.position;
    }
}