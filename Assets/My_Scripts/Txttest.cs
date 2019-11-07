using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Txttest : MonoBehaviour
{
    public static int[] ballsValue;
    public int ball; 
    public string test;
    public string[] teststr;
    int j = 1; 

    // Start is called before the first frame update
    void Start()
    {
        test = "halldikkdi";
        ballsValue = new int[] { 2, 2, 3, 2, 5 };
        ball = 5;
         
    }

    // Update is called once per frame
    void Update()
    {
        if (j == 1)
        {
            teststr = new string[ballsValue.Length];

            test = ball.ToString();
            for (int i = 0; i < ballsValue.Length; i++)
            {
                teststr[i] = ballsValue[i].ToString();
            }

            test = String.Join(",", teststr);

            System.IO.File.WriteAllText(@"C:\Users\Charlotte\Documents\Test.txt", test);
        }
        j++; 
    }
}
