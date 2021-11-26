using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Enum containing each team of adventurers
public enum Team
{
    Blue,
    Red,
    Green,
    Yellow
}

//Characters include adventurers and the dragon
public class Character : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;

    //The location where the character is immediately heading
    [SerializeField] protected Vector2 destinationPoint;

    //Distance at which a character is considered to have reached their destination
    protected float arrivalDistance = 5.01f;
    public int currentPatrolPoint = 0;

    [SerializeField] public GameObject locationsManager;

    //Location of the dragon's treasure hoard
    [SerializeField] public Transform hoardLocation;

    //Locations surrounding the hoard where the dragon patrols
    [SerializeField] public List<Transform> dragonPatrolPoints;

    [SerializeField] float carryingSpeed = 3f;
    [SerializeField] float normalSpeed = 4.5f;

    public bool reachedDestination = false;

    //Characters can carry one treasure at a time
    public Treasure carriedTreasure = null;

    [SerializeField] public Team team;

    // Set up references to components and get important locations from locationsManager
    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        locationsManager = GameObject.Find("LocationsManager");

        hoardLocation = GameObject.Find("HoardPoint").transform;
        dragonPatrolPoints = locationsManager.GetComponent<LocationsManager>().dragonPatrolPoints;
    }

    //Set the character's destination using its navmesh
    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.destination = new Vector3(destination.x, transform.position.y, destination.z);
        reachedDestination = false;
    }

    //Check if the character has reached its destination by comparing its position to that of the destination
    public void CheckDestination()
    {
        if (Vector3.Distance(navMeshAgent.destination, new Vector3(transform.position.x, navMeshAgent.transform.position.y - transform.localScale.y, transform.position.z)) < 2f)
        {
            reachedDestination = true;
        }
    }

    //Pick up a treasure object
    //A character can only carry one piece of treasure at a time
    public void PickUpTreasure(Treasure treasure)
    {
        if (carriedTreasure == null && treasure.beingCarried == false)
        {
            treasure.PickedUp(this);
            navMeshAgent.speed = carryingSpeed;
        }
    }

    //Place treasure at a deposit location and score one point
    public void DepositTreasure()
    {
        if (carriedTreasure != null)
        {
            Treasure depositedTreasure = carriedTreasure;
            carriedTreasure.gameObject.SetActive(true);
            carriedTreasure.Deposited(this);
            navMeshAgent.speed = normalSpeed;

            locationsManager.GetComponent<LocationsManager>().AddToScore(team);

            //Disable tresure when at deposit point so it doesn't get counted twice
            depositedTreasure.gameObject.SetActive(false);
        }
    }

    //Eat someone (likely an adventurer)
    public void Eat(Character food)
    {
        food.Die();
    }
    
    //Leave this earth (likely as a result of being eaten)
    public void Die()
    {   
        //Drop any carried treasure
        if (carriedTreasure != null)
        {
            carriedTreasure.gameObject.SetActive(true);
            carriedTreasure.Deposited(this);
        }

        gameObject.SetActive(false);
    }
}
