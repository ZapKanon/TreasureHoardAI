using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> adventurers;
    [SerializeField] List<GameObject> dragons;

    [SerializeField] MeshCollider viewCone;
    [SerializeField] GameObject lastHit;
    [SerializeField] Vector3 collisionLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(this.transform.position, transform.forward * 100, Color.red);
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (this.name == "Dragon")
    //    {
    //        if (!CheckLineOfSight(collision))
    //        {
    //            Debug.Log(gameObject.name + " sees an adventurer: " + collision.gameObject.name);
    //        }
    //    }
            
        
        
    //    else if (collision.gameObject.tag == "Dragon")
    //    {
    //        //if (CheckLineOfSight(collision))
    //        //Debug.Log(gameObject.name + " sees a dragon: " + collision.gameObject.name);
    //    }
    //}

    //After detecting a collision with a view cone, check if anything intercepts a raycast to the target
    //private bool CheckLineOfSight(Collider collision)
    //{
    //    //Ray ray = new Ray(this.transform.position, this.transform.position - collision.gameObject.transform.position);
    //    //RaycastHit hit;

    //    //if (Physics.Raycast(ray, out hit, 40))
    //    //{
    //    //    Debug.Log(hit.transform.gameObject);
    //    //    //If the thing we hit is the thing we were checking for
    //    //    if (hit.transform.gameObject == collision.transform.gameObject)
    //    //    {
    //    //        lastHit = hit.transform.gameObject;
    //    //        collisionLocation = hit.point;
    //    //        return true;
    //    //    }
    //    //}

    //    //return false;
    //    //Debug.DrawLine(transform.position, collision.gameObject.transform.position, Color.green, 1000);
    //    if (Physics.Linecast(transform.position, collision.gameObject.transform.position, out RaycastHit hitInfo))
    //    {
    //        Debug.Log("Blocked");
    //        Debug.DrawLine(this.transform.position, hitInfo.transform.gameObject.transform.position, Color.red, 10);
        
    //        return false;
    //    }

    //    return true;
    //}
}
