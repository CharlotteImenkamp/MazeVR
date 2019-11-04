using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfig
{
    public int scene_index;
    public bool ballsref1;
    public bool ballsref2; 
    

    public MapConfig(int scene_index, bool ballsref1, bool ballsref2)
    {
        this.scene_index = scene_index;
        this.ballsref1 = ballsref1;
        this.ballsref2 = ballsref2; 
    }
}
