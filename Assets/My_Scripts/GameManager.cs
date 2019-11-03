using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    //menu
    bool menuflag;
    public bool path_active;

    //time
    public float t_block;
    public float t_left;

    //Aufzeichnungen
    public int[] ballsValue; 
    public int[] sicknessValue;
    public int[] immersionValue; 

    // all maps *** brauche ich das noch? ***
    MapConfig[] maps = new MapConfig[] {
        new MapConfig(1, true), //0
        new MapConfig(1, false), //1
        new MapConfig(2, true), //2
        new MapConfig(2, false), //3
        new MapConfig(3, true), //4
        new MapConfig(3, false) //5
    };

    // Lists
    int[] mapOrder = new int[] { 0, 1, 2, 3, 4 , 5, 6 }; // mache pseudoran ***
    public int currentListIdx = 0;

    // Awake Singelton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start Find Objects, open StartMenu
    void Start()
    {
        // time
        t_block = 10f;          // s
        t_left = t_block;
        menuflag = true;

        //Pseudoran
        // Liste auf Pseudoranfkt***

        SceneManager.LoadScene("StartMenu");
    }

    private void Update()
    {
        t_left -= Time.deltaTime;

        // abhilfe für notVRstuff. Nur drücken, wenn start button vorhanden
        if (Input.GetKeyDown("space"))
        {
            print("spacepressed");
            StartLevelKey();
        }

        // wenn Zeit rum, lade Menü und stoppe timer
        if (t_left <= 0 && menuflag == false)
        {
            SceneManager.LoadScene("SliderMenu");
            menuflag = true;

        }

        //wenn currentListIdx == AnzBlöcke -1 ***
        //dann speicher anz. bälle, sickness und immersion value und spieler id! ***
    }


    //StartButton clicked
    public void StartLevel()
    {
        t_left = t_block;
        menuflag = false;

        //set new map active
        MapConfig current = maps[mapOrder[currentListIdx]];
        SceneManager.LoadScene(current.scene_index);
        path_active = current.path_active;

        //go on
        currentListIdx++;
    }

    public void StartLevelKey()
    {
        t_left = t_block;
        menuflag = false;

        //set new map active
        MapConfig current = maps[mapOrder[currentListIdx]];
        SceneManager.LoadScene(current.scene_index);
        path_active = current.path_active;

        //go on
        currentListIdx++;
    }

    //Methode Pseudorandom ***

}


