using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfig
{
    public int scene_index;
    public bool path_active; 

    public MapConfig(int scene_index, bool path_active)
    {
        this.scene_index = scene_index;
        this.path_active = path_active; 
    }
}
