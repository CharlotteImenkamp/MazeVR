using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float t_elapsed; // in seconds
    private float t_block; 


    void Start()
    {
        t_elapsed = 0.0f;
        t_block = 125.0f;
        
    }


    void Update()
    {
        t_elapsed += Time.deltaTime;
        if (t_elapsed > t_block)
        {
            t_elapsed -= t_block;
        }
        
    }
}
