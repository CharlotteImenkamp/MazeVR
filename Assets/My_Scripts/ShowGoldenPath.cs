using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMesh


public class ShowGoldenPath : MonoBehaviour
{
    private NavMeshAgent agent;
    public NavMeshPath path;
    private float elapsed = 0.0f;

    

    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.SetColors(Color.white, Color.white);

        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
        elapsed = 0.0f;
       

    }

      void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position, GameObject.Find("GoldBall1").transform.position, NavMesh.AllAreas, path);
            lineRenderer.positionCount = path.corners.Length;
        }

        for (int j = 0; j < path.corners.Length  ; j++)
        {
            lineRenderer.SetPosition(j, path.corners[j]);
            print(j);
        }

    }

}

