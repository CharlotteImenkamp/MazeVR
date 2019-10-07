﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
 public class Movement : MonoBehaviour
 {
     public Vector2 trackpad;
     public Vector3 moveDirection = new Vector3(0, 0, 0);
 

     //public SteamVR_Input_Sources Hand;//Set Hand To Get Input From
     //public float speed;
     public GameObject Head;
    // public CapsuleCollider Collider;
     public GameObject AxisHand;//Hand Controller GameObject
     public float Deadzone;//the Deadzone of the trackpad. used to prevent unwanted walking.
     // Start is called before the first frame update


     private CapsuleCollider CapCollider;
     public PhysicMaterial NoFrictionMaterial;
     public SteamVR_Input_Sources MovementHand;//Set Hand To Get Input From
     private Rigidbody rb;
     private void Start()
    {
        CapCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        updateInput();
        updateCollider(); //brauche ich das?

        //get the angle of the touch and correct it for the rotation of the controller
        moveDirection = Quaternion.AngleAxis(Angle(trackpad) + AxisHand.transform.localRotation.eulerAngles.y, Vector3.up) * Vector3.forward;

        Vector3 velocity = new Vector3(0, 0, 0); //??

        if (trackpad.magnitude > Deadzone && trackpad.x+trackpad.y != 0)
        {//make sure the touch isn't in the deadzone and we aren't going to fast or zero
            CapCollider.material = NoFrictionMaterial;
            rb.velocity = moveDirection;

        }
     }
     public static float Angle(Vector2 p_vector2)
     {
         if (p_vector2.x < 0)
         {
             return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
         }
         else
         {
             return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
         }
     }


     private void updateInput()
     {
         trackpad = TrackpadAction.GetAxis(MovementHand); ;
     }

    private void updateCollider()
    {
        CapCollider.height = Head.transform.localPosition.y;
        CapCollider.center = new Vector3(Head.transform.localPosition.x, Head.transform.localPosition.y / 2, Head.transform.localPosition.z);
    }
}

 
