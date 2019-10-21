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

    public string name; 
  
    void Start()
    {

        // Line Renderer
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        //lineRenderer.startWidth = 0.5f;
        //lineRenderer.endWidth = 0.01f;
        //lineRenderer.startColor = Color.blue;
        //lineRenderer.endColor = Color.blue;
        //lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
  

        // Generate Path
        path = new NavMeshPath();
        elapsed = 0.0f;

        // set player
        player = "FirstPerson-AIO";
        //player = "[CameraRig]";
    }

    void Update()
    {

            // Update the way to the target every 0.5 second.
            elapsed += Time.deltaTime;
            if (elapsed > updateIntervall)
            {
                elapsed = 0.0f;
                name = FindNearestObj();

            NavMesh.CalculatePath(
                GameObject.Find(player).transform.position,      // Player pos
                GameObject.Find(name).transform.position,       // Current Ball pos
                NavMesh.AllAreas,
                path
            );

            for (int i = 0; i < path.corners.Length; i++)
                {
                    path.corners[i] += pathOffset;
                }
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

            // calculate Path length 
            for (int j = 0; j < path.corners.Length - 1; j++)
            {
                pathlength = pathlength + Vector3.Distance(path.corners[j], path.corners[j + 1]);
            }
            
            // set minpath to smallest path length
            if (pathlength < minpath)
            {
                minpath = pathlength;
                name = obj.name;
            }
        }
        return name; 
    }







}

