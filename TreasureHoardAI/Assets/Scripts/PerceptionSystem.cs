using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        worldState = new List<KeyValuePair<string, object>>();
        StartCoroutine(FOVRoutine());
        //Debug.Log(LayerMask.NameToLayer("Adventurer"));
        locationsManager = GameObject.Find("LocationsManager").GetComponent<LocationsManager>();
        character = GetComponent<Character>();
    }

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

            if (adventurersInView.Count > 0)
            {
                worldState.Add(new KeyValuePair<string, object>("seesAdventurer", true));

                if (Vector3.Distance(transform.position, FindClosestTarget(adventurersInView).transform.position) < 5.0f)
                {
                    worldState.Add(new KeyValuePair<string, object>("nearAdventurer", true));
                }
            }

            if (dragonsInView.Count > 0)
            {
                worldState.Add(new KeyValuePair<string, object>("seesDragon", true));
            }

            if (treasureInView.Count > 0)
            {
                worldState.Add(new KeyValuePair<string, object>("seesTreasure", true));
            }

            //Update world state based on position

            if (Vector3.Distance(transform.position, locationsManager.hoardPoint.position) < 2.0f)
            {
                worldState.Add(new KeyValuePair<string, object>("atHoard", true));
            }
            
            else if (Vector3.Distance(transform.position, locationsManager.depositPoints[(int)character.team].position) < 2.0f)
            {
                worldState.Add(new KeyValuePair<string, object>("atDepositPoint", true));
            }

            //Update world state based on having treasure picked up

            if (character.carriedTreasure != null)
            {
                worldState.Add(new KeyValuePair<string, object>("hasTreasure", true));
            }
        }
    }

    private void FieldOfViewCheck(LayerMask targetMask, string targetType)
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                if (rangeChecks[i].gameObject != gameObject)
                {
                    Transform target = rangeChecks[i].transform;
                    Vector3 directionToTarget = (target.position - transform.position).normalized;

                    if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, target.position);

                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        {
                            targetInView = true;
                            Debug.Log(name + " sees " + target.name);
                            Debug.DrawLine(transform.position, target.position, Color.green, 0.2f);
                            if (targetType == "Adventurer")
                            {
                                adventurersInView.Add(target.gameObject);
                            }
                            else if (targetType == "Dragon")
                            {
                                dragonsInView.Add(target.gameObject);
                            }
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
        //Target is no longer in view
        else if(targetInView)
        {
            targetInView = false;
        }
    }

    //From a list of target objects, find the closest one
    public GameObject FindClosestTarget(List<GameObject> targetsList)
    {
        GameObject closest = null;
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
        Debug.Log(this.gameObject.name + " is closest to " + closest.gameObject.name);

        return closest;
    }
}
