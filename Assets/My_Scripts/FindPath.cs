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
        if (GetCurrentBall())
        {
            // Update the way to the target every 0.5 second.
            elapsed += Time.deltaTime;
            if (elapsed > updateIntervall)
            {
                elapsed = 0.0f;
                NavMesh.CalculatePath(
                    GameObject.Find("FirstPerson-AIO").transform.position,      // Player pos
                    GameObject.Find(GetCurrentBall().name).transform.position,  // Current Ball pos
                    NavMesh.AllAreas,
                    path
                );
                for(int i = 0; i < path.corners.Length; i++)
                {
                    path.corners[i] += pathOffset;
                }
                lineRenderer.positionCount = path.corners.Length;
                lineRenderer.SetPositions(path.corners);
            }
        }
    }

    private GameObject GetCurrentBall()
    {
        return GManager.Instance.BallList[0];
    }







}

