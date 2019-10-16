using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject collision;

    private void Start()
    {
        //collision = GameObject.Find("collision");

        //collision.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoldBall"))
        {
            other.gameObject.SetActive(false);
            collision.SetActive(true);
        }
    }
}
