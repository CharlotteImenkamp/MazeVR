using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    GameObject startMenu;
    GameObject sliderMenu;
    GameObject countMenu;
    GameObject Map_Menu; 
    public GameObject player; 

    GameObject test; 

    public float t_block = 10f;         // s
    public float t_left = 0f;             // s

    // Lists
    public List<GameObject> mapList;
    int[] mapOrder = new int[] { 0, 1, 0, 1,0,1,0,1,0,1,0,1,0,1 };
    public int currentListIdx = 0; 

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

    void Start()
    {
        // get menu Objects 
        startMenu = GameObject.Find("StartMenu");
        sliderMenu = GameObject.Find("SliderMenu");
        countMenu = GameObject.Find("CounterMenu");
        Map_Menu = GameObject.Find("Map_Menu");

        StartMainMenu();

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
            mapList[mapOrder[currentListIdx]].SetActive(false);
            StartMainMenu();
        }
    }

    public void RegisterMap(GameObject map)
    {
        if (map)
        {
            mapList.Add(map);
        }
    }

    // StartButton clicked
    public void StartLevel() 
    {


        //mapList[mapIndex].SetActive(true);
        //mapIndex++;
        t_left = t_block;
    }

    public void StartLevelKey()
    {
        currentListIdx++;
        mapList[mapOrder[currentListIdx]].SetActive(true);

        RemoveMainMenu();

        t_left = t_block; 
    }

    public void StartMainMenu()
    {
        //player.transform.position = GameObject.Find("spawnpoint").transform.position;

        // set startMenu Active
        startMenu.SetActive(true);
        sliderMenu.SetActive(false);
        countMenu.SetActive(false);

        // set Map_Menu active
        for (int i = 0; i < mapList.Count; i++)
        {
                mapList[i].SetActive(false);
        }

        Map_Menu.SetActive(true); 

    }

    public void RemoveMainMenu()
    {
        startMenu.SetActive(false);
        Map_Menu.SetActive(false);
    }

    

}


