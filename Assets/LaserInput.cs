using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInput : MonoBehaviour
{
    public static GameObject currentObject;
    int currentID; 

    void Start()
    {
        currentObject = null;
        currentID = 0; 
    }


    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f); 

        for(int i= 0; i< hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            print(hit);

            int id = hit.collider.gameObject.GetInstanceID();

            if(currentID != id)
            {
                currentID = id;
                currentObject = hit.collider.gameObject;

                string name = currentObject.name; 
                if(name == "Next")
                {
                    Debug.Log("Hit next"); 
                }

                string tag = currentObject.tag; 

                if(tag == "Button")
                {
                    Debug.Log("HitButton"); 
                }
            }
        }
    }
}
