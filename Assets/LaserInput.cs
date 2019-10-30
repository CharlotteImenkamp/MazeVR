using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class LaserInput : MonoBehaviour
{
    public static GameObject currentObject;
    int currentID;
    string currentName;
    public SteamVR_Input_Sources m_TargetSouce;
    public SteamVR_Action_Boolean m_ClickAction;


    void Start()
    {
        currentObject = null;
        currentID = 0;
        currentName = ""; 
    }

    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);

        //press
        if (m_ClickAction.GetStateDown(m_TargetSouce))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.name == "StartButton")
                {
                    GameManager.Instance.StartLevel();
                }
            }
        }
    }
}
