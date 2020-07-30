using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScene : MonoBehaviour
{
    public string LevelName = "test";
    // Use this for initialization
    void Start()
    {
        if (LevelName.Length > 0)
        {
            Application.LoadLevelAdditive(LevelName);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
