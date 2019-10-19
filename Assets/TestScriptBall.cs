using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GManager.Instance.RegisterBall(gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
    }
}
