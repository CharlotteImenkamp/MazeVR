using System; 
using System.Collections;
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

    //Aufzeichnungen
    public int[] ballsValue; 
    public List<int> sicknessValue;
    public List<int> immersionValue;

    // Lists
    public int[] mapOrder;
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

        // mapOrder
        mapOrder = new int[] { 0, 1, 2, 3, 4, 5 };

        // Nur wenn richtiges Experiment. Sonst egal.
        if (mapOrder.Length == 6)
        {
            mapOrder = Shuffle(mapOrder);
        }

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

    //Methode Pseudorandom*******
    private int[] Shuffle(int[] newOrder)
    {

        //immer abwechselnd
        //int temp;
        //bool swap = true;
        //int nswap = 0;
        //int stop = (newOrder.Length / 2);

        //shuffle
        //for (int i = 0; i < newOrder.Length; i++)
        //{
        //    int rnd = UnityEngine.Random.Range(0, newOrder.Length);

        //    temp = newOrder[rnd];
        //    newOrder[rnd] = newOrder[i];
        //    newOrder[i] = temp;
        //}

        //pseudorandom
        //while (swap == true && nswap < 5)
        //{
        //    swap = false;

        //    for (int j = 1; j < newOrder.Length - 2; j++)
        //    {
        //        mittlerer vertauscht mit random
        //        if (newOrder[j] < stop)
        //        {
        //            if (newOrder[j - 1] < stop && newOrder[j + 1] < stop)
        //            {
        //                int rnd = UnityEngine.Random.Range(0, newOrder.Length);

        //                temp = newOrder[j];
        //                newOrder[j] = newOrder[rnd];
        //                newOrder[rnd] = temp;
        //                swap = true;
        //            }
        //        }
        //    }
        //    nswap += 1;

        //    if (nswap == 5)
        //    {
        //        print("***Achtung, keine reihenfolge gefunden ***");
        //    }
        //}
        return newOrder;
    }

    //funktioniert in Testscene
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


