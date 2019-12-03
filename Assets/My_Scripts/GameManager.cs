using System; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public string SpielerID = "3";

    //menu
    bool menuflag;
    bool ruheflag;
    bool labyflag; 
    public bool start_easyKond;

    //time
    public float t_ruhe;
    public float t_block;
    public float t_left;

    //lists
    public int[] ballsValue; 
    public List<int> sicknessValue;
    public List<int> immersionValue;

    public int[] mapOrder;
    public int[] newOrder;
    public int[] mapeasy;
    public int[] maphard; 
    public int currentListIdx;

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
        labyflag = false; 
        ruheflag = true; 

        currentListIdx = 0;

        // time
        t_ruhe = 3f;
        t_block = 30f;          // s
        t_left = t_block;

        // mapOrder + shuffle
        start_easyKond = false; 

        mapeasy = new int[] { 0, 1 };
        maphard = new int[] { 2, 3 };
        mapOrder = new int[mapeasy.Length + maphard.Length];
        mapOrder = Shuffle();
        mapOrder[0] = 1;


        //Aufzeichnung
        ballsValue = new int[mapOrder.Length+1];
        sicknessValue = new List<int>();
        immersionValue = new List<int>();

        //Start
        SceneManager.LoadScene("Ruhemessung");
        
    }

    private void Update()
    {
        // Starte mit Ruhemessung
        if (ruheflag)
        {
            t_ruhe -= Time.deltaTime;

            if (t_ruhe <= 0)
            {
                SceneManager.LoadScene("SliderMenu");
                menuflag = true;
                ruheflag = false;
                labyflag = true; 
            }
        }

        // Nach der Ruhemessung folgt Labyrinth und Frageraum im Wechsel
        if (labyflag)
        {
            t_left -= Time.deltaTime;

            // Wenn Zeit rum, lade Menü und Stoppe timer
            if (t_left <= 0 && menuflag == false)
            {
                SceneManager.LoadScene("SliderMenu");
                menuflag = true;
            }
        }    

        // ***abhilfe für notVRstuff. Nur drücken, wenn start button vorhanden***löschen vor Anhang
        if (Input.GetKeyDown("space"))
        {
            StartLevelKey();
        }
    }

    //StartButton clicked
    public void StartLevel()
    {
        t_left = t_block;
        menuflag = false;

        //set new map active
        SceneManager.LoadScene(mapOrder[currentListIdx]+1);

        //go on
        currentListIdx++;
    }

    public void StartLevelKey()
    {
        t_left = t_block;
        menuflag = false;

        //set new map active
        SceneManager.LoadScene(mapOrder[currentListIdx]);
        
        //go on
        currentListIdx++;
    }

    //method pseudorandom
    public int[] Shuffle()
    {
        newOrder = new int[mapeasy.Length + maphard.Length];
        int temp;
        int e = 0;
        int h = 0;

        // Randomize easy
        for (int i = 0; i < mapeasy.Length; i++)
        {
            int rnde = UnityEngine.Random.Range(0, mapeasy.Length);

            temp = mapeasy[rnde];
            mapeasy[rnde] = mapeasy[i];
            mapeasy[i] = temp;
        }

        // Randomize hard
        for (int i = 0; i < mapeasy.Length; i++)
        {
            int rndh = UnityEngine.Random.Range(0, maphard.Length);

            temp = maphard[rndh];
            maphard[rndh] = maphard[i];
            maphard[i] = temp;
        }

        for(int i=0; i<newOrder.Length; i++)
        {
            if (start_easyKond == true)
            {
                newOrder[i] = mapeasy[e];
                e++;
                start_easyKond = false; 
            }
            else if (start_easyKond == false)
            {
                newOrder[i] = maphard[h];
                h++;
                start_easyKond = true; 
            }
        }

        return newOrder;
    }

   //write in txt file
  public void WriteTxt()
    {
        string [] ballsValueStr = new string[ballsValue.Length];
        string[] immersionValueStr = new string[ballsValue.Length];
        string[] sicknessValueStr = new string[ballsValue.Length];
        string temp; 
     
        for (int i = 0; i < ballsValue.Length; i++)
        {
            ballsValueStr[i] = ballsValue[i].ToString();
            sicknessValueStr[i] = sicknessValue[i].ToString();
            immersionValueStr[i] = immersionValue[i].ToString();
        }

        //ballsValue
        temp = String.Join(",", ballsValueStr);
        System.IO.File.WriteAllText(@"C:\Users\Charlotte\Documents\VR_Behav\Proband" + SpielerID + "_balls.txt", temp);

        //immersionValue
        temp = String.Join(",", immersionValueStr);
        System.IO.File.WriteAllText(@"C:\Users\Charlotte\Documents\VR_Behav\Proband" + SpielerID + "_immersion.txt", temp);

        //sickness
        temp = String.Join(",", sicknessValueStr);
        System.IO.File.WriteAllText(@"C:\Users\Charlotte\Documents\VR_Behav\Proband" + SpielerID + "_sickness.txt", temp);
    }
}
