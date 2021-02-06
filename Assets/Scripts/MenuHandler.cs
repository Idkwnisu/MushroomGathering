using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    private static MenuHandler _instance;

    public GameObject resumePanel;

    public static MenuHandler Instance { get { return _instance; } }

    public bool paused = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();   
        }
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        resumePanel.SetActive(true);
        paused = true;
    }

    public void TogglePause()
    {
        if(paused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        resumePanel.SetActive(false);
        paused = false;
    }

    public void Exit()
    {
        //Save here?
        Application.Quit();
    }
}
