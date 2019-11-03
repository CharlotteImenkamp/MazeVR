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

    public int currentListIdx; 

    void Start()
    {
        currButtSick = null;
        currButtImm = null;
        currObj = null;
        currentListIdx = GameManager.Instance.currentListIdx; 
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
                            currButtImm.GetComponent<Image>().color = Color.white;
                        }

                        // Aktualisiere Button 
                        currButtImm = currObj.gameObject;

                        // Aktiviere Button
                        currButtImm.GetComponent<Image>().color = new Color(145f, 145f, 145f);
                        break;

                    case "ButtonSickness":
                        // Setze alten Button zurück
                        if (currButtSick != null)
                        {
                            currButtSick.GetComponent<Image>().color = Color.white;
                        }

                        // Aktualisiere Button 
                        currButtSick = currObj.gameObject;

                        // Aktiviere Button
                        currButtSick.GetComponent<Image>().color = new Color(145f, 145f, 145f);
                        break;

                    case "StartButton":
                        if (currButtSick != null && currButtImm != null)
                        {
                            // Sende Buttoninhalte an GameManager
                            GameManager.Instance.sicknessValue[currentListIdx]
                                = int.Parse(currButtSick.GetComponentInChildren<Text>().text);
                            GameManager.Instance.immersionValue[currentListIdx]
                                = int.Parse(currButtImm.GetComponentInChildren<Text>().text);

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
