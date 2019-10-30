using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

//Eventsystem, which came with Canvas x
//remove standalone input  x
//add this script  x
//rename eventmanager to PR_VRInputModule  x
//make as prefab  x
// add prefab to PR_Pointer at right hand into Input Module slot  x
// add standart sprite default material to line renderer at pr_pointer x
//canvas to world space
// add pr_pinter (camera) to canvas event camera  x

public class VRInputModule : BaseInputModule
{
    // test1
    public static VRInputModule Instance { get;}

    //test2
    public PointerEventData m_test = null;

    public Camera m_Camera;
    public SteamVR_Input_Sources m_TargetSouce;
    public SteamVR_Action_Boolean m_ClickAction;

    private GameObject m_CurrentObject = null;
    public PointerEventData m_Data = null;

    protected override void Awake()
    {
        base.Awake();

        m_Data = new PointerEventData(eventSystem);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("KeyDown");
            PointerEventData hh = GetData();
            print(hh);
            print("dfsefsefsef");
        }
    }

    public override void Process()
    {
        // Reset data, set camera
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);

        // Raycast 
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

        // Clear Raycast
        m_RaycastResultCache.Clear();

        // handle hover states
        HandlePointerExitAndEnter(m_Data, m_CurrentObject);

        // press
        if (m_ClickAction.GetStateDown(m_TargetSouce))
        {
            ProcessPress(m_Data);
        }

        // release
        if (m_ClickAction.GetStateUp(m_TargetSouce))
        {
            ProcessRelease(m_Data);
        }
        
        Debug.Log("Process nach if: " + m_Data);
    }

    public PointerEventData GetData()
    {
        Debug.Log("getdata: " + m_Data);
        //return m_Data;

        //test2
        return m_test; 
    }

    private void ProcessPress(PointerEventData data)
    {
        // set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        // check for object hit, get the down handler, call 
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

        // if no down handler, try and get click handler
        if (newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);
        }

        // set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;

        Debug.Log("Processpress: " +  data); 

    }
    private void ProcessRelease(PointerEventData data)
    {
        // execute pointer up 
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // check for click handler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // check if actual
        if (data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);

        }

        // cear selected gameObject
        eventSystem.SetSelectedGameObject(null);

        // reset data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;

        Debug.Log("Processrelease: " + data);
    }
}

// set open spaces x
// camera : PR_Pointer(Camera) x
// targetSouce RightHand x
// ClickAction: Teleport oder anderes x
