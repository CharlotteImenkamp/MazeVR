using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBackup : MonoBehaviour
{
    public static GameManagerBackup Instance { get; set; }
    //menu
    GameObject startMenu;
    GameObject sliderMenu;
    GameObject countMenu;
    GameObject Map_Menu;

    //player
    public GameObject player;
    Vector3 transform1;
    Vector3 transform2;
    Vector3 transformmenu;

    //time
    public float t_block;         // s
    public float t_left;          // s

    // Lists
    public List<GameObject> mapList;
    int[] mapOrder = new int[] { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };
    public int currentListIdx = 0;

    // Awake Singelton
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

    // Start Find Objects, open StartMenu
    void Start()
    {
        // get menu Objects 
        startMenu = GameObject.Find("StartMenu");
        sliderMenu = GameObject.Find("SliderMenu");
        countMenu = GameObject.Find("CounterMenu");
        Map_Menu = GameObject.Find("Map_Menu");

        // time
        t_block = 10f;
        t_left = 10f;

        // set maps inactive
        foreach (GameObject obj in mapList)
        {
            obj.SetActive(false);
        }

        //transform
        transform1 = new Vector3(-26.5f, 3.4f, -6.6f);
        transform2 = new Vector3(28f, 3.4f, -6.6f);
        transformmenu = new Vector3(9.2f, 3.4f, 4f);

        // StartMenu
        RemoveMenu();
        StartMenu("start");
    }

    private void Update()
    {
        t_left -= Time.deltaTime;

        if (Input.GetKeyDown("space"))
        {
            print("spacepressed");
            StartLevelKey();
        }

        if (t_left <= 0)
        {
            //aktuelle map aus
            mapList[mapOrder[currentListIdx]].SetActive(false);

            //start slider menu
            StartMenu("slider");
        }
    }

    public void RegisterMap(GameObject map)
    {
        if (map)
        {
            mapList.Add(map);
        }
    }

    //StartButton clicked
    //public void StartLevel()
    //{
    //    mapList[mapIndex].SetActive(true);
    //    mapIndex++;
    //    t_left = t_block;
    //}

    public void StartLevelKey()
    {
        currentListIdx++;
        t_left = t_block;

        //set new map active
        mapList[mapOrder[currentListIdx]].SetActive(true);

        //set new player position
        if (mapList[mapOrder[currentListIdx]].name == "maze1")
        {
            player.transform.position = transform1;
        }
        if (mapList[mapOrder[currentListIdx]].name == "Map2")
        {
            player.transform.position = transform2;
        }

        //spawnpoint = mapList[mapOrder[currentListIdx]].transform.Find("spawnpoint").gameObject;
        //player.transform.position = spawnpoint.transform.position;

        //remove menu
        RemoveMenu();
    }

    public void StartMenu(string selection)
    {
        //start Map
        Map_Menu.SetActive(true);

        //set new player position
        //spawnpoint = Map_Menu.transform.Find("spawnpoint").gameObject;
        //player.transform.position = spawnpoint.transform.position;
        player.transform.position = transformmenu;

        if (selection == "start")
        {
            //öffne start menü
            startMenu.SetActive(true);
        }
        else if (selection == "slider")
        {
            //öffne slider Menü
            sliderMenu.SetActive(true);
        }
        else
        {
            print("menüwahl nicht bekannt. Startmenu.selection");
        }

    }

    public void RemoveMenu()
    {
        //set all menus false
        startMenu.SetActive(false);
        sliderMenu.SetActive(false);
        countMenu.SetActive(false);

        Map_Menu.SetActive(false);
    }



}