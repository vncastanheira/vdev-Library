using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vnc.Tools;

public class CheatsTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        CheatManager.RegisterCommand("test", Test);
    }

    public void Test()
    {
        Debug.Log("Test successfull");
    }
}
