using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;

    //The location where the character is immediately heading
    protected Vector2 destinationPoint;

    //Distance at which a character is considered to have reached their destination
    protected float arrivalDistance = 5.01f;

    [SerializeField] public GameObject locationsManager;

    //Location of the dragon's treasure hoard
    [SerializeField] public Transform hoardLocation;

    //Locations surrounding the hoard where the dragon patrols
    [SerializeField] public List<Transform> dragonPatrolPoints;

    public bool reachedDestination = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        locationsManager = GameObject.Find("LocationsManager");

        hoardLocation = GameObject.Find("HoardPoint").transform;
        dragonPatrolPoints = locationsManager.GetComponent<LocationsManager>().dragonPatrolPoints;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.destination = new Vector3(destination.x, transform.position.y, destination.z);
        reachedDestination = false;
    }

    public void CheckDestination()
    {
        if (Vector3.Distance(navMeshAgent.destination, transform.position) < 0.1f)
        {
            reachedDestination = true;
        }
    }
}
