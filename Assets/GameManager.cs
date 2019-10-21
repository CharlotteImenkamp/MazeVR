using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public bool Oddball;

    public bool FindPath;

    public int t_block;         //in seconds

    public List<GameObject> mapList;

    // register active player

    public GameObject startMenu;
    public GameObject count1Menu;
    public GameObject count2Menu;
    public GameObject count3Menu;
    public GameObject sliderMenu;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Reset()
    {
        // set default values
        t_block = 120;

        //set maps inactive
        for (int i = 0; i < mapList.Count; i++)
        {
            mapList[i].SetActive(false); 
        }

        startMenu = GameObject.Find("StartMenu");
        sliderMenu = GameObject.Find("SliderMenu");
        count1Menu = GameObject.Find("1");
        count2Menu = GameObject.Find("2");
        count3Menu = GameObject.Find("3");

        startMenu.SetActive(false);
        sliderMenu.SetActive(false);
        count1Menu.SetActive(false);
        count2Menu.SetActive(false);
        count3Menu.SetActive(false);
    }

    public void Start()
    {


        // Berechne reihenfolge der labys und aufgaben. Pseudorandomisiert
        // startbildschirm mit button start
        // warte auf startpress

        // laufe reihenfolgen vektor ab
        // countdown start
        // starte laby und oddball
        // starte evtl FindPath
        // countdown stop
        // abfrage

        mapList[0].SetActive(true);
    }

    public void RegisterMap(GameObject map)
    {
        if (map)
        {
            mapList.Add(map);
        }
    }
}
