using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> adventurersInView;
    [SerializeField] List<GameObject> dragonsInView;
    [SerializeField] List<GameObject> treasureInView;

    public float radius;
    public float angle;

    public LayerMask adventMask;
    public LayerMask dragonMask;
    public LayerMask treasureMask;

    public LayerMask obstructionMask;

    public bool targetInView;
    public GameObject activeTarget;
    public string activeTargetType;

    //Characters can carry one treasure at a time
    public bool hasTreasure = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FOVRoutine());
        //Debug.Log(LayerMask.NameToLayer("Adventurer"));
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            adventurersInView = new List<GameObject>();
            dragonsInView = new List<GameObject>();
            treasureInView = new List<GameObject>();

            FieldOfViewCheck(adventMask, "Adventurer");
            FieldOfViewCheck(dragonMask, "Dragon");
            FieldOfViewCheck(treasureMask, "Treasure");
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
                            if(targetType == "Adventurer")
                            {
                                adventurersInView.Add(target.gameObject);
                            }
                            else if(targetType == "Dragon")
                            {
                                dragonsInView.Add(target.gameObject);
                            }
                            else if(targetType == "Treasure")
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
}
