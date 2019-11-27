using UnityEngine;
using UnityEngine.UI; 
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
        cbr.normalColor = Color.grey;
        cbr.colorMultiplier = 1; 

        currButtSick = null;
        currButtImm = null;
        currObj = null;
        currentListIdx = GameManager.Instance.currentListIdx;
    }

    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);

        // activete laser
        if (m_ClickAction.GetStateDown(m_TargetSouce))
        {
            // search in hitList
            for (int i = 0; i < hits.Length; i++)
            {
                // set current Object 
                currObj = hits[i].collider.gameObject;

                switch (currObj.tag)
                {
                    case "ButtonImmersion":
                        //reset old button
                        if (currButtImm != null)
                        {
                            currButtImm.GetComponent<Button>().colors = cbw;
                        }

                        // refresh button
                        currButtImm = currObj.gameObject;

                        // activate button
                        currButtImm.GetComponent<Button>().colors = cbr;
                        break;

                    case "ButtonSickness":
                        // reset old button 
                        if (currButtSick != null)
                        {
                            currButtSick.GetComponent<Button>().colors = cbw; ;
                        }

                        // refresh button 
                        currButtSick = currObj.gameObject;

                        // activate button
                        currButtSick.GetComponent<Button>().colors = cbr;
                        break;

                    case "StartButton":
                        if (currButtSick != null && currButtImm != null)
                        {
                            // sent button content to GameManager
                            GameManager.Instance.sicknessValue.Add(int.Parse(currButtSick.name));

                            GameManager.Instance.immersionValue.Add(int.Parse(currButtImm.name));

                            //***********check*************************
                            if (currentListIdx == GameManager.Instance.mapOrder.Length)
                            {
                                print("writetxt");
                                GameManager.Instance.WriteTxt();
                            }

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
