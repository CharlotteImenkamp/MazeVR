using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Inactivate", 2f);
        print("nach inactivate");
    }

    void Inactivate()
    {
        print("inaktivate");
    }
}
