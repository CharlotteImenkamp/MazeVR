using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMesh


public class FindPath : MonoBehaviour
{
    public float updateIntervall = 0.5f;
    private float elapsed = 0.0f;

    private LineRenderer lineRenderer;

    public NavMeshPath path;

    public Vector3 pathOffset = new Vector3();

    public string player;

    public string nextObj; 
  
    void Start()
    {

        // Line Renderer
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Generate Path
        path = new NavMeshPath();
        elapsed = 0.0f;

        // set player
        player = "FirstPerson-AIO";
        //player = "player";
        //player = "[CameraRig]";
    }

    void Update()
    {

            // Update the way to the target every 0.5 second.
            elapsed += Time.deltaTime;
            if (elapsed > updateIntervall)
            {
                elapsed = 0.0f;
                nextObj = FindNearestObj();

                NavMesh.CalculatePath(
                    GameObject.Find(player).transform.position,      // Player pos
                    GameObject.Find(nextObj).transform.position,       // Current Ball pos
                    NavMesh.AllAreas,
                    path
                );

                for (int i = 0; i < path.corners.Length; i++)
                {
                    path.corners[i] += pathOffset;
                }
                print(path.corners.Length); 

                lineRenderer.positionCount = path.corners.Length;
                lineRenderer.SetPositions(path.corners);
            }
        
        
    }

    private string FindNearestObj()
    {
        float pathlength = 0.0f;
        float minpath = 100000.0f;
   

        foreach (GameObject obj in GManager.Instance.BallList)
        {
            pathlength = 0.0f;

            // calculate Path
            NavMesh.CalculatePath(
                GameObject.Find(player).transform.position,      // Player pos
                GameObject.Find(obj.name).transform.position,    // Current Ball pos
                NavMesh.AllAreas,
                path
            );
            print(obj.name + " :" + obj.transform.position);

            // calculate Path length 
            for (int j = 0; j < path.corners.Length - 1; j++)
            {
                pathlength = pathlength + Vector3.Distance(path.corners[j], path.corners[j + 1]);
            }
            
            // set minpath to smallest path length
            if (pathlength < minpath)
            {
                minpath = pathlength;
                nextObj = obj.name;
            }
        }

        
        return nextObj; 
    }







}

