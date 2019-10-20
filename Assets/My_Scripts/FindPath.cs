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

    public int i; 

    //public GameObject closest;
    //public GameObject collision;


    void Start()
    {

        // Line Renderer
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        //lineRenderer.startWidth = 0.5f;
        //lineRenderer.endWidth = 0.01f;
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
  

        // Generate Path
        path = new NavMeshPath();
        elapsed = 0.0f;
    }

    void Update()
    {

            // Update the way to the target every 0.5 second.
            elapsed += Time.deltaTime;
            if (elapsed > updateIntervall)
            {
                elapsed = 0.0f;
                path = FindNearestPath();

            for (int i = 0; i < path.corners.Length; i++)
                {
                    path.corners[i] += pathOffset;
                }
                lineRenderer.positionCount = path.corners.Length;
                lineRenderer.SetPositions(path.corners);
            }
        
    }

    public NavMeshPath FindNearestPath()
    {
        float pathlength = 0.0f;
        float minpath = 100000.0f;
        NavMeshPath dummiepath = new NavMeshPath();
        int i = 0; 
        foreach (GameObject obj in GManager.Instance.BallList)
        {
            pathlength = 0.0f;

            // calculate Path
            NavMesh.CalculatePath(
                GameObject.Find("FirstPerson-AIO").transform.position,      // Player pos
                GameObject.Find(obj.name).transform.position,               // Current Ball pos
                NavMesh.AllAreas,
                dummiepath
            );

            // calculate Path length 
            for (int j = 0; j < dummiepath.corners.Length - 1; j++)
            {
                pathlength = pathlength + Vector3.Distance(dummiepath.corners[j], dummiepath.corners[j + 1]);
            }
            
            // set minpath to smallest path length
            if (pathlength < minpath)
            {
                minpath = pathlength;
                path = dummiepath;
            }

        }

        print("shortestpath: " + path.corners.Length);
        return path; 
    }







}

