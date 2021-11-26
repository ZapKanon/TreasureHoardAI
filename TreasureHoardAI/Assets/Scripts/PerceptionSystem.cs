using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Each character possesses a perception system which informs their world state
//This state is used to make decisions between pursuing different goals
//Characters perceive the world through a cone of vision extending in front of them
public class PerceptionSystem : MonoBehaviour
{
    [SerializeField] public List<GameObject> adventurersInView;
    [SerializeField] public List<GameObject> dragonsInView;
    [SerializeField] public List<GameObject> treasureInView;

    public float radius;
    public float angle;

    public LayerMask adventMask;
    public LayerMask dragonMask;
    public LayerMask treasureMask;

    public LayerMask obstructionMask;

    public bool targetInView;
    public GameObject activeTarget;
    public string activeTargetType;

    [SerializeField] public LocationsManager locationsManager;

    [SerializeField] public List<KeyValuePair<string, object>> worldState;

    private Character character;

    //Initialize, get references, and start the FOV coroutine
    void Start()
    {
        worldState = new List<KeyValuePair<string, object>>();
        StartCoroutine(FOVRoutine());
        //Debug.Log(LayerMask.NameToLayer("Adventurer"));
        locationsManager = GameObject.Find("LocationsManager").GetComponent<LocationsManager>();
        character = GetComponent<Character>();
    }

    //Every 0.2 seconds, run view cone checks for each set of relevant objects
    //Update world state based on objects found and other factors
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;

            worldState = new List<KeyValuePair<string, object>>();

            adventurersInView = new List<GameObject>();
            dragonsInView = new List<GameObject>();
            treasureInView = new List<GameObject>();

            FieldOfViewCheck(adventMask, "Adventurer");
            FieldOfViewCheck(dragonMask, "Dragon");
            FieldOfViewCheck(treasureMask, "Treasure");

            //Update world state based on objects perceived

            //ADVENTURERS
            if (adventurersInView.Count > 0)
            {
                worldState.Add(new KeyValuePair<string, object>("seesAdventurer", true));

                if (Vector3.Distance(transform.position, FindClosestTarget(adventurersInView).transform.position) < 5.0f)
                {
                    worldState.Add(new KeyValuePair<string, object>("nearAdventurer", true));
                }
            }

            //DRAGONS
            if (dragonsInView.Count > 0)
            {
                worldState.Add(new KeyValuePair<string, object>("seesDragon", true));
            }

            //TREASURE
            if (treasureInView.Count > 0)
            {
                worldState.Add(new KeyValuePair<string, object>("seesTreasure", true));
            }

            //Update world state based on position relative to important places

            //DISTANCE TO HOARD
            if (Vector3.Distance(transform.position, locationsManager.hoardPoint.position) < 2.0f)
            {
                worldState.Add(new KeyValuePair<string, object>("atHoard", true));
            }
            
            //DISTANCE TO TEAM'S DEPOSIT POINT
            else if (Vector3.Distance(transform.position, locationsManager.depositPoints[(int)character.team].position) < 2.0f)
            {
                worldState.Add(new KeyValuePair<string, object>("atDepositPoint", true));
            }

            //Update world state based on having treasure picked up
            if (character.carriedTreasure != null)
            {
                worldState.Add(new KeyValuePair<string, object>("hasTreasure", true));
            }

            //Update scores / check if the game should end
            locationsManager.GetComponent<LocationsManager>().UpdateScores();
        }
    }

    //Detect objects within the view cone
    private void FieldOfViewCheck(LayerMask targetMask, string targetType)
    {
        //Create a sphere around the character that checks for objects on a certain layer
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        //If at least one target object is detected
        if(rangeChecks.Length != 0)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                //Do not detect self
                if (rangeChecks[i].gameObject != gameObject)
                {
                    Transform target = rangeChecks[i].transform;
                    
                    //Determine the direction vector from self to target
                    Vector3 directionToTarget = (target.position - transform.position).normalized;

                    //Check if the target is within a certain angle in front of the character
                    //This results in a view cone where targets can be perceived
                    if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, target.position);

                        //Once a target is confirmed to be within the view cone, perform a raycast
                        //If the raycast returns false, there aren't any walls blocking view of the target
                        //If there ARE walls in the way, the character ignores the target since thay can't "see" them
                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        {
                            //Confirm that target is visible
                            targetInView = true;
                            
                            //Add the target to the correct list based on what they are

                            //ADVENTURER
                            if (targetType == "Adventurer")
                            {
                                //Debug lines to test dragon behavior
                                //if (this.name == "Dragon")
                                //{
                                //    Debug.Log(name + " sees " + target.name);
                                //    Debug.DrawLine(transform.position, target.position, Color.green, 0.2f);
                                //}
                                adventurersInView.Add(target.gameObject);
                            }

                            //DRAGON
                            else if (targetType == "Dragon")
                            {
                                dragonsInView.Add(target.gameObject);
                            }

                            //TREASURE
                            else if (targetType == "Treasure" && target.GetComponent<Treasure>().beingCarried == false)
                            {
                                treasureInView.Add(target.gameObject);
                            }
                        }
                        else
                        {
                            targetInView = false;
                        }
                    }
                    else
                    {
                        targetInView = false;
                    }
                }
            }
        }
        //Target was in view, but isn't anymore
        else if(targetInView)
        {
            targetInView = false;
        }
    }

    //From a list of target objects, find the closest one
    public GameObject FindClosestTarget(List<GameObject> targetsList)
    {
        GameObject closest = null;
        //Start closest distance at a very high value so it will be overwritten
        float closestDistance = 5000f;

        //Loop through the list to find the closest object
        foreach (GameObject target in targetsList)
        {
            if (closest == null || Vector3.Distance(transform.position, target.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
            {
                closest = target;
                closestDistance = Vector3.Distance(transform.position, target.transform.position);
            }
        }

        return closest;
    }
}
