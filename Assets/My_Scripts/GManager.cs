using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager Instance { get; set; }

    public List<GameObject> BallList;
    public bool BallDestroyed;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this; 
        }
    }

}
