using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; set; }

    public List<GameObject> BallList;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this; 
        }else
        {
            Destroy(this.gameObject);
        }
    }

    public void Reset()
    {
        BallList = new List<GameObject>();
    }

    public void RegisterBall(GameObject ball)
    {
        if(ball)
        {
            BallList.Add(ball);
        }
    }

    public void RemoveBall(GameObject ball)
    {
        GManager.Instance.BallList.Remove(ball);
        Destroy(ball);
    }

}
