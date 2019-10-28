using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CollectBall : MonoBehaviour
{
    //Controller Input
    public SteamVR_Action_Boolean m_GrabAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;

    //Grab Behaviour
    private FixedJoint m_Joint = null; 

    // GameObject new
    private GameObject m_currentObj = null;

    //Awake
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        //m_Joint = GetComponent<FixedJoint>();
    }

    //Updata
    void Update()
    {


        //Trigger Down
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print("Trigger Down");
            Pickup();
        }
        //Trigger Up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print("Trigger up");
            Drop();
        }
    }

    //Add to List
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("GoldBall"))
        {
            return;
        }

        m_currentObj = other.gameObject;
        //m_contactBall.Add(other.gameObject.GetComponent<Interactable>());
    }

    //Remove from List
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("GoldBall"))
        {
            return;
        }

        m_currentObj = null;
    }


    public void Pickup()
    {
        //get nearest Interactable
        //m_currentBall = GetNearestInteractable();

        //Nullcheck
        if (!m_currentObj)
        {
            return;
        }

        //Position
        m_currentObj.transform.position = transform.position;
        m_currentObj.transform.SetParent(transform);

    }

    public void Drop()
    {
        //Null check
        if (!m_currentObj)
        {
            return;
        }

        //destroy object
        Destroy(m_currentObj);

        //clear
        m_currentObj = null;
    }
}