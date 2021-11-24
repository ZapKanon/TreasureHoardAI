using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum Team
{
    Blue,
    Red,
    Green,
    Yellow
}

public class Character : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;

    //The location where the character is immediately heading
    protected Vector2 destinationPoint;

    //Distance at which a character is considered to have reached their destination
    protected float arrivalDistance = 5.01f;
    public int currentPatrolPoint = 0;

    [SerializeField] public GameObject locationsManager;

    //Location of the dragon's treasure hoard
    [SerializeField] public Transform hoardLocation;

    //Locations surrounding the hoard where the dragon patrols
    [SerializeField] public List<Transform> dragonPatrolPoints;

    public bool reachedDestination = false;

    //Characters can carry one treasure at a time
    public Treasure carriedTreasure = null;

    [SerializeField] public int team;

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
        if (this.gameObject.name != "Dragon")
        {
            //Debug.Log("Dragon is going here: " + navMeshAgent.destination + " but is sitting here: " + new Vector3(transform.position.x, navMeshAgent.transform.position.y - transform.localScale.y, transform.position.z));
        }

        if (Vector3.Distance(navMeshAgent.destination, new Vector3(transform.position.x, navMeshAgent.transform.position.y - transform.localScale.y, transform.position.z)) < 2f)
        {
            reachedDestination = true;
        }
    }

    public void PickUpTreasure(Treasure treasure)
    {
        if (carriedTreasure == null && treasure.beingCarried == false)
        {
            treasure.PickedUp(this);
        }
    }

    public void DepositTreasure()
    {
        if (carriedTreasure != null)
        {
            carriedTreasure.gameObject.SetActive(true);
            carriedTreasure.Deposited(this);
        }
    }
}
