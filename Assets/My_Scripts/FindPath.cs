using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMesh


public class FindPath : MonoBehaviour
{
    public NavMeshPath path;
    private float elapsed = 0.0f;
    public Vector3 ballPos;
    public Vector3 playPos;

    //public GameObject closest;
    //public GameObject collision;

    public List<GameObject> BallList;
    public GameObject currentBall;
    public int i; 


    void Start()
    {
        i = 0;
        ballPos = new Vector3();
        playPos = new Vector3();

        // Line Renderer
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
  

        // Generate Path
        path = new NavMeshPath();
        elapsed = 0.0f;

        //    ballsVect = GameObject.FindGameObjectsWithTag("GoldBall");
        //    currentBall = ballsVect[0].name;

        // Make list and copy to GManager
        BallList = new List<GameObject>(); 
        foreach(GameObject BallObj in GameObject.FindGameObjectsWithTag("GoldBall"))
        {
            BallList.Add(BallObj);
        }
        GManager.Instance.BallList = BallList;

        currentBall = BallList[0];
    }

    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        //update List
        BallList = GManager.Instance.BallList;

        //Go through List
        if (GManager.Instance.BallDestroyed)
        {
            
            // setze neuen Ball
            currentBall = BallList[0];

            GManager.Instance.BallDestroyed = false; 
        }

        // Set Positions
        ballPos = GameObject.Find(currentBall.name).transform.position;
       playPos = GameObject.Find("FirstPerson-AIO").transform.position;
        // Update the way to the target every 0.5 second.
        elapsed += Time.deltaTime;
        if (elapsed > 0.5f)
        {
            elapsed -= 0.5f;
            NavMesh.CalculatePath(playPos, ballPos, NavMesh.AllAreas, path);
            lineRenderer.positionCount = path.corners.Length;
        }
        for (int j = 0; j < path.corners.Length; j++)
        {

            lineRenderer.SetPosition(j, path.corners[j]);
        }
    }







}

