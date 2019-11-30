using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class Movement : MonoBehaviour
{
    public Vector2 trackpad;
    public Vector3 moveDirection = new Vector3(0, 0, 0);
    public float speed = 0.8f;

    public GameObject Head;
    public GameObject AxisHand;     //Hand Controller GameObject
    public float Deadzone;          //the Deadzone of the trackpad

    private CapsuleCollider CapCollider;
    public PhysicMaterial NoFrictionMaterial;
    public SteamVR_Input_Sources MovementHand;      //Set Hand To Get Input From
    private Rigidbody rb;

    public SteamVR_Action_Boolean m_press = null;



    private void Start()
    {
        CapCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        updateInput();
        updateCollider();

        //get the angle of the touch and correct it for the rotation of the controller 
        //if (SteamVR_Actions._default.Squeeze.GetActive) {
        //    speed = 2;
        //    print(SteamVR_Actions._default.Squeeze.active); 
        //}
        //else
        //{
        //    speed = 1; 
        //}

        moveDirection = Quaternion.AngleAxis(Angle(trackpad) + AxisHand.transform.localRotation.eulerAngles.y, Vector3.up) * Vector3.forward * speed;

        rb.velocity = new Vector3(0, 0, 0);
        if (trackpad.magnitude > Deadzone && trackpad.x + trackpad.y != 0)
        {
            //make sure the touch isn't in the deadzone and we aren't going to fast or zero
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

    private void updateCollider()
    {
        CapCollider.height = Head.transform.localPosition.y;
        //CapCollider.center = new Vector3(Head.transform.localPosition.x, Head.transform.localPosition.y / 2, Head.transform.localPosition.z);
        CapCollider.center = Head.transform.localPosition;    
    }

    private void updateInput()
    {
        trackpad = SteamVR_Actions._default.MovementAxis.GetAxis(MovementHand);
    }
}


