using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

//add rigigbody and colloder with trigger to object
//add collider to hand

public class CollectBall : MonoBehaviour
{
    //Controller Input
    public SteamVR_Action_Boolean m_GrabAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;

    // GameObject new
    private GameObject m_currentObj = null;

    public int currListidx;

    //Awake
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void Start()
    {
        currListidx = GameManager.Instance.currentListIdx; 
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
        //Nullcheck
        if (!m_currentObj)
        {
            return;
        }

        //Position
        m_currentObj.transform.SetParent(transform);
    }

    public void Drop()
    {
        //Null check
        if (!m_currentObj)
        {
            return;
        }

        //destroy object and count to list
        GameManager.Instance.ballsValue[currListidx] += 1; 
        Destroy(m_currentObj);

        //clear
        m_currentObj = null;
    }
}