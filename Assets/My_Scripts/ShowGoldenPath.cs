using System.Collections;
using UnityEngine;
using UnityEngine.AI; //NavMesh


public class klj: MonoBehaviour
{
    public NavMeshPath path;
    private float elapsed = 0.0f;
    public Vector3 ballPos;
    public Vector3 playPos;
    public GameObject closest;
    public GameObject[] ballsVect;
    public GameObject collision;
    public string currentBall = "";
    // hier vllt länge einbauen

    void Start()
    {

        // Line Renderer
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.startColor = Color.white; 
        lineRenderer.endColor = Color.white;
        lineRenderer.material = new Material(Shader.Find("Particles/Standard Surface"));

        // Generate Path
        path = new NavMeshPath();
        elapsed = 0.0f;
        
        ballsVect = GameObject.FindGameObjectsWithTag("GoldBall");
        currentBall = ballsVect[0].name;

        ballPos = new Vector3();
        playPos = new Vector3();
        

    }

    void Update()
    {
        currentBall = SelectBall(collision, ballsVect);

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        ballPos = GameObject.Find(currentBall).transform.position;
        playPos = GameObject.Find("FirstPerson-AIO").transform.position;


        // Update the way to the goal every 0.5 second.
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

    public string SelectBall(GameObject collision, GameObject[] ballsVect)
    {
        int i = 0;
        if (collision.activeSelf)
        {
            i = i + 1; 
            print("isactive");
            currentBall = ballsVect[i].name;
            print(ballsVect[i].name);
            
            collision.SetActive(false);
        }

        return currentBall;
    }




}

