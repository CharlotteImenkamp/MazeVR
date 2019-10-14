using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInput : MonoBehaviour
{
    public static GameObject currentObject;
    int currentID;

    // Start is called before the first frame update
    void Start()
    {
        currentObject = null;
        currentID = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, 100.0f))
        {

            if (hit.collider.name == GameConstants.CHARACTER_ID)
            {
                return true;
            }
        }



        ////sends out a raycast and returns an array filled with everything it hit
        //// distance: 100 adapt if neccessary
        //RaycastHit[] hits;
        //hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);

        ////Goes through all the hit objects and checks if any of them were our button 
        //for (int i= 0; i< hits.Length; i++)
        //{
        //    RaycastHit hit = hits[i];

        //    //use the object id to determine if code already ran for this object
        //    int id = hit.collider.gameObject.GetInstanceID();

        //    // If not, run it again
        //    if (currentID != id)
        //    {
        //        currentID = id;
        //        currentObject = hit.collider.gameObject;

        //        // checks based of the tag  
        //        string tag = currentObject.tag; 
        //        if (tag == "GoldBall")
        //        {
        //            Debug.Log("tag found");
        //        }
        //    }
        //}

    }
}



