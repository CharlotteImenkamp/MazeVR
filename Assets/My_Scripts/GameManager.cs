using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    //Bälle
    public bool ballsref1;
    public bool ballsref2; 

    //menu
    bool menuflag;

    //time
    public float t_block;
    public float t_left;

    //Aufzeichnungen
    public int[] ballsValue; 
    public List<int> sicknessValue;
    public List<int> immersionValue;

    //scene_index, ballsref1, ballsref2
    MapConfig[] maps = new MapConfig[] {
        //mit pfad
        new MapConfig(0, true, false), //0
        new MapConfig(0, false, true),
        new MapConfig(1, true, false),
        new MapConfig(1, false, true),
        new MapConfig(2, true, false),
        new MapConfig(2, false, true),
        new MapConfig(3, true, false),
        new MapConfig(3, false, true),
        new MapConfig(4, true, false),
        new MapConfig(4, false, true),
        new MapConfig(5, true, false),
        new MapConfig(5, false, true),

        //ohne pfad
        //new MapConfig(6, true, false),
        //new MapConfig(6, false, true),
        //new MapConfig(7, true, false),
        //new MapConfig(7, false, true),
        //new MapConfig(8, true, false),
        //new MapConfig(8, false, true),
        //new MapConfig(9, true, false),
        //new MapConfig(9, false, true),
        //new MapConfig(10, true, false),
        //new MapConfig(10, false, true),
        //new MapConfig(11, true, false),
        //new MapConfig(11, false, true),
    };

    // Lists
    public int[] mapOrder;
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
        menuflag = true;
        print(maps.Length);

        //Pseudorandomisation***
        for (int i = 0; i < maps.Length; i++)
        {
            print(i);
            mapOrder[i] = i;
        }

        //Aufzeichnung
        ballsValue = new int[mapOrder.Length];
        sicknessValue = new List<int>();
        immersionValue = new List<int>();

        // time
        t_block = 60f;          // s
        t_left = t_block;



        //mapOrder = Shuffle(mapOrder);

        SceneManager.LoadScene("StartMenu");
    }

    private void Update()
    {
        t_left -= Time.deltaTime;

        // abhilfe für notVRstuff. Nur drücken, wenn start button vorhanden
        if (Input.GetKeyDown("space"))
        {
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

        //activate Ballsconfig (BallsRef1 und 2)***
        //ballsref1 = current.ballsref1;
        //ballsref2 = current.ballsref2;
        
        //go on
        currentListIdx++;
    }

    //Methode Pseudorandom ***
    private int[] Shuffle(int[] newOrder)
    {
        int temp; 

        for(int i=0; i < newOrder.Length; i++)
        {
            int rnd = Random.Range(0, newOrder.Length);

            temp = newOrder[rnd];
            newOrder[rnd] = newOrder[i];
            newOrder[i] = temp; 
        } 
        return newOrder; 
    }
}


