using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.RegisterMap(gameObject);
    }

}
