using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (transform.root.gameObject.name == "Dragon")
        {
            //Debug.DrawLine(this.transform.position, collision.transform.position, Color.red, 10);
            Debug.Log(this.gameObject.name + " sees an adventurer: " + collision.gameObject);
        }       
    }
}
