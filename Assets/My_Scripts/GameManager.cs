using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
//menu
    bool menuflag; 

    //time
    public float t_block;        
    public float t_left;          

 // Lists
    int[] mapOrder = new int[] { 1,  2,  1,  2,  1,  2,  1};
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
        t_left = 10f;
        menuflag = true;

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
    }


    //StartButton clicked
    public void StartLevel()
    {
    }

    public void StartLevelKey()
    {
        t_left = t_block;
        menuflag = false;

        //set new map active
        SceneManager.LoadScene(mapOrder[currentListIdx]);
        currentListIdx++;
    }

    public void RemoveMenu()
    {

    }

    



}


