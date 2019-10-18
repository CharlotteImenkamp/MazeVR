using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Handtest : MonoBehaviour
{
    //Controller Input
    public SteamVR_Action_Boolean m_GrabAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;

    //Grab Behaviour
    private FixedJoint m_Joint = null;

    ////Interactable
    //private Interactable m_currentBall = null;
    //public List<Interactable> m_contactBall = null;       

    // GameObject new
    private GameObject m_currentObj = null;

    //Awake
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
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
            return;

        m_currentObj = other.gameObject;
        //m_contactBall.Add(other.gameObject.GetComponent<Interactable>());
    }

    //Remove from List
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("GoldBall"))
            return;

        m_currentObj = null;
        //m_contactBall.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        //get nearest Interactable
        //m_currentBall = GetNearestInteractable();

        //Nullcheck
        if (!m_currentObj)  //vorher: m_currentBall
        {
            return;
        }

        //Position
        //m_currentBall.transform.position = transform.position;
        m_currentObj.transform.position = transform.position;

        //Attatch to fixed joint
        Rigidbody targedbody = m_currentObj.GetComponent<Rigidbody>(); //vorher: currentBall
        m_Joint.connectedBody = targedbody;
    }

    public void Drop()  //new: alles was obj war zu ball
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

    //private Interactable GetNearestInteractable()
    //{
    //    Interactable nearest = null;
    //    float minDistance = float.MaxValue;
    //    float distance = 0.0f;

    //    foreach (Interactable ball in m_contactBall)
    //    {
    //        distance = (ball.transform.position - transform.position).sqrMagnitude;

    //        if (distance < minDistance)
    //        {
    //            minDistance = distance;
    //            nearest = ball;
    //        }
    //    }
    //    return nearest;
    //}
}