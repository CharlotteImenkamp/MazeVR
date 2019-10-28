using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManRef : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(GameManager.Instance.path_active);
    }
}
