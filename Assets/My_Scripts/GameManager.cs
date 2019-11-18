using System; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public string SpielerID = "1";

    //menu
    bool menuflag;
    public bool start_easyKond; 

    //time
    public float t_block;
    private float t_left;

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

        currentListIdx = 0;

        // time
        t_block = 10f;          // s
        t_left = t_block;

        // mapOrder + shuffle
        start_easyKond = false; 

        mapeasy = new int[] { 0, 1, 2 };
        maphard = new int[] { 3, 4, 5 };
        mapOrder = new int[mapeasy.Length + maphard.Length];
        mapOrder = Shuffle();


        //Aufzeichnung
        ballsValue = new int[mapOrder.Length];
        sicknessValue = new List<int>();
        immersionValue = new List<int>();

        //Start
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
    }

    //StartButton clicked
    public void StartLevel()
    {
        t_left = t_block;
        menuflag = false;

        //set new map active
        SceneManager.LoadScene(mapOrder[currentListIdx]);

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
        System.IO.File.WriteAllText(@"C:\Users\Charlotte\Documents\Proband" + SpielerID + "_balls.txt", temp);

        //immersionValue
        temp = String.Join(",", immersionValueStr);
        System.IO.File.WriteAllText(@"C:\Users\Charlotte\Documents\Proband" + SpielerID + "_immersion.txt", temp);

        //sickness
        temp = String.Join(",", sicknessValueStr);
        System.IO.File.WriteAllText(@"C:\Users\Charlotte\Documents\Proband" + SpielerID + "_sickness.txt", temp);
    }
}
