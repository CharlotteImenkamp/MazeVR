using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject Canvas_3;
    public GameObject Canvas_2;
    public GameObject Canvas_1;


    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.t_left <=3 && GameManager.Instance.t_left > 2)
        {
            Canvas_3.SetActive(true); 
        }

        if (GameManager.Instance.t_left <= 2 && GameManager.Instance.t_left > 1)
        {
            Canvas_3.SetActive(false);
            Canvas_2.SetActive(true);
        }

        if (GameManager.Instance.t_left <= 1 && GameManager.Instance.t_left > 0)
        {
            Canvas_2.SetActive(false);
            Canvas_1.SetActive(true);
        }


    }
}
