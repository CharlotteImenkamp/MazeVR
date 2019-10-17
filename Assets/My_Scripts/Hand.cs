using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;
    private Interactable m_currentBall = null;
    public List<Interactable> m_contactBall = null; 
    
    // Start is called before the first frame update
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // down
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print("Trigger Down");
            Pickup();
        }
        // up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print("Trigger up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("GoldBall"))
            return;

        m_contactBall.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("GoldBall"))
            return;

        m_contactBall.Remove(other.gameObject.GetComponent<Interactable>());

    }

    public void Pickup()
    {
        //get nearest Interactable
        m_currentBall = GetNearestInteractable();

        //Nullcheck
        if (!m_currentBall)
        {
            return;
        }

        //Position
        m_currentBall.transform.position = transform.position;

        //Attatch to fixed joint
        Rigidbody targedbody = m_currentBall.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targedbody; 

        //set active hand ? 
       

    }

    public void Drop()
    {

        //Null check
        if (!m_currentBall)
        {
            return;
        }

        //destroy object
        Destroy(m_currentBall);

        //clear
        m_currentBall = null; 

    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f; 

        foreach(Interactable ball in m_contactBall)
        {
            distance = (ball.transform.position - transform.position).sqrMagnitude;

            if(distance < minDistance)
            {
                minDistance = distance;
                nearest = ball;
            }
        }
        return nearest; 
    }
}
