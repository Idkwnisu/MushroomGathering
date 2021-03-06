﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SaveManager.Instance.loadGame)
        {
            SaveManager.Instance.LoadGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMushroomScene()
    {
        SceneManager.LoadScene("MushroomScene");
    }

    public void StartWithTime(bool night)
    {
        GenerationValuesManager.Instance.night = night;
        GenerationValuesManager.Instance.currentAreaActive = 0; //temp
        StartMushroomScene();
    }

    public void StartWithArea(int area)
    {
        Time.timeScale = 1.0f;
        GenerationValuesManager.Instance.currentAreaActive = area;
        StartMushroomScene();
    }
}
