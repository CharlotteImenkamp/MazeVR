using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoldBall"))
        {
            BallManager.Instance.RemoveBall(other.gameObject);
        }
    }
}
