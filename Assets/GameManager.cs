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

    public GameObject menu;



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
    //}

    //public void Reset()
    //{

    }

    public void Start()
    {
        // set default values
        t_block = 120;

        // set menu

        menu = GameObject.Find("Menu");


        //set maps inactive
        foreach (GameObject map in mapList)
        {
            map.SetActive(false);
        }

        //set menu inactive
        menu.SetActive(false);




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
