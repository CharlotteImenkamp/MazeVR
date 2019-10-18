using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoldBall"))
        {
            GManager.Instance.BallList.Remove(other.gameObject);
            Destroy(other.gameObject);
            GManager.Instance.BallDestroyed = true; 
        }
    }
}
