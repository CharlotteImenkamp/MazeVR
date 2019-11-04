using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using Valve.VR;

public class LaserInput : MonoBehaviour
{
    public SteamVR_Input_Sources m_TargetSouce;
    public SteamVR_Action_Boolean m_ClickAction;

    public GameObject currButtSick;
    public GameObject currButtImm;
    public GameObject currObj;

    ColorBlock cbw;
    ColorBlock cbr;

    public int currentListIdx; 

    void Start()
    {
        cbw = new ColorBlock();
        cbw.normalColor = Color.white;
        cbw.colorMultiplier = 1; 

        cbr = new ColorBlock();
        cbr.normalColor = Color.red;
        cbr.colorMultiplier = 1; 

        currButtSick = null;
        currButtImm = null;
        currObj = null;
        currentListIdx = GameManager.Instance.currentListIdx;
        
        // change button color to sth nicer ***
    }

    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);

        // Aktiviere Laser
        if (m_ClickAction.GetStateDown(m_TargetSouce))
        {
            // Durchsuche HitList
            for (int i = 0; i < hits.Length; i++)
            {
                // Setze aktuelles Objekt  
                currObj = hits[i].collider.gameObject;

                switch (currObj.tag)
                {
                    case "ButtonImmersion":
                        // Setze alten Button zurück
                        if (currButtImm != null)
                        {
                            currButtImm.GetComponent<Button>().colors = cbw;
                        }

                        // Aktualisiere Button 
                        currButtImm = currObj.gameObject;

                        // Aktiviere Button
                        currButtImm.GetComponent<Button>().colors = cbr;
                        break;

                    case "ButtonSickness":
                        // Setze alten Button zurück
                        if (currButtSick != null)
                        {
                            currButtSick.GetComponent<Button>().colors = cbw; ;
                        }

                        // Aktualisiere Button 
                        currButtSick = currObj.gameObject;

                        // Aktiviere Button
                        currButtSick.GetComponent<Button>().colors = cbr;
                        break;

                    case "StartButton":
                        if (currButtSick != null && currButtImm != null)
                        {
                            // Sende Buttoninhalte an GameManager
                            GameManager.Instance.sicknessValue.Add(int.Parse(currButtSick.name));

                            GameManager.Instance.immersionValue.Add(int.Parse(currButtImm.name));

                            // Start Level
                            GameManager.Instance.StartLevel();
                        }
                        else if (currentListIdx == 0)
                        {
                            // Start Level
                            GameManager.Instance.StartLevel();
                        }
                        break; 

                }
            }
        }
    }
}
