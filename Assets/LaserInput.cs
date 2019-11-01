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

    //neu current button immersion***
    // neu current button sickness***

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

                if(hits[i].collider.gameObject.tag == "ButtonImmersion")
                {
                    // Wenn currentImmButton = empty Färbe den Button ein***
                    
                    // Wenn currenImmbutton != empty färbe Button ein und reset anderen Button und setze currentbutton neu ***
                }

                if (hits[i].collider.gameObject.tag == "Sickness")
                {
                    // Wenn currentSickButton = empty Färbe den Button ein***

                    // Wenn currentSickbutton != empty färbe Button ein und reset anderen Button und setze currentbutton neu ***
                }

                if (hits[i].collider.gameObject.name == "StartButton") //und currentSickbutton !=empty && currentImmButton != empty***
                {
                    //Sende Buttoninhalte an GameManager***
                    GameManager.Instance.StartLevel();
                }
            }
        }
    }
}
