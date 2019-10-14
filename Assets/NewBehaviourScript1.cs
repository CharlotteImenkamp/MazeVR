using UnityEngine;
using System.Collections;

public class DrawLineAgain : MonoBehaviour
{

    private int ClickCount = 0;
    private Vector2[] clicks = new Vector2[100];
    private GameObject[] lines = new GameObject[100];
    private LineRenderer[] newLine = new LineRenderer[100];
    private Object[] pointArr = new Object[101];
    private GameObject point;
    public bool ifMaskBtnClicked = false;
    public bool ifAutoMaskBtnClicked = false;
    private bool ifResetClicked = false;

    void Start()
    {

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = new GameObject();
        }
        for (int i = 0; i < lines.Length; i++)
        {
            newLine[i] = lines[i].AddComponent<LineRenderer>();
            newLine[i].SetWidth(0.1f, 0.1f);
        }
        point = GameObject.Find("/Point");
    }



    public void Update()
    {
        if (ifMaskBtnClicked)
        {
            if (ifResetClicked)
            {
                print("reinitialise");
                ClickCount = 0;
                pointArr = new Object[101];
                clicks = new Vector2[100];
                lines = new GameObject[100];
                newLine = new LineRenderer[100];
                pointArr = new Object[101];

                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = new GameObject();
                }
                for (int i = 0; i < lines.Length; i++)
                {
                    newLine[i] = lines[i].AddComponent<LineRenderer>();
                    newLine[i].SetWidth(0.1f, 0.1f);
                }
                point = GameObject.Find("/Point");

            }
            if (Input.GetMouseButtonDown(0))
            {
                while (ClickCount < lines.Length)
                {

                    print("ClickCount : " + ClickCount);

                    if (ClickCount == 0)
                    {
                        clicks[ClickCount] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                        clicks[ClickCount] = Camera.main.ScreenToWorldPoint(clicks[ClickCount]);
                        pointArr[ClickCount] = Instantiate(point, clicks[ClickCount], Quaternion.identity);
                        ClickCount++;
                        break;
                    }
                    else
                    {
                        clicks[ClickCount] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                        clicks[ClickCount] = Camera.main.ScreenToWorldPoint(clicks[ClickCount]);
                        newLine[ClickCount].SetPosition(0, clicks[ClickCount - 1]);
                        newLine[ClickCount].SetPosition(1, clicks[ClickCount]);
                        pointArr[ClickCount] = Instantiate(point, clicks[ClickCount], Quaternion.identity);
                        ClickCount++;
                        break;
                    }

                }
            }



            if (Input.GetKeyDown(KeyCode.A))
            {
                setAutoMaskBtnClicked(true);

            }
        }


        if (ifAutoMaskBtnClicked)
        {
            print("ifAutoMaskBtnClicked : ClickCount " + (ClickCount - 1));
            clicks[ClickCount] = clicks[ClickCount - 1];
            //clicks[ClickCount] = Camera.main.ScreenToWorldPoint(clicks[ClickCount]);
            newLine[ClickCount].SetPosition(0, clicks[ClickCount]);
            newLine[ClickCount].SetPosition(1, clicks[0]);
            //Instantiate(point,clicks[ClickCount],Quaternion.identity);
            ifAutoMaskBtnClicked = false;
            ifMaskBtnClicked = false;
        }

    }


    public void Reset()
    {
        ifResetClicked = true;
        ifAutoMaskBtnClicked = false;
        ifMaskBtnClicked = false;


        for (int i = 0; i < lines.Length; i++)
        {
            DestroyObject(pointArr[i]);
            //DestroyImmediate(newLine);
            newLine[i].SetVertexCount(0);
        }
    }



    public void setMaskBtnClicked(bool value)
    {
        ifMaskBtnClicked = true;
    }
    public void setAutoMaskBtnClicked(bool value)
    {
        ifAutoMaskBtnClicked = true;
    }


}




/*
            if (firstTouch)
            {
                firstClick = new Vector2(Input.mousePosition.x , Input.mousePosition.y );
                firstClick = Camera.main.ScreenToWorldPoint(firstClick);
                firstTouch = false;
            }
            else
            {
                Vector2 secondClick = new Vector2(Input.mousePosition.x , Input.mousePosition.y );
                secondClick = Camera.main.ScreenToWorldPoint(secondClick);
                newLine.SetPosition(0, firstClick);
                newLine.SetPosition(1, secondClick);
                firstTouch = true;
            }*/
