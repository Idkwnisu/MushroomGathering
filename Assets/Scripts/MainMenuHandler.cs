using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public Button loadGame;
    // Start is called before the first frame update
    void Start()
    {
        if(!SaveManager.Instance.ExistSavefile())
        {
            loadGame.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SaveManager.Instance.loadGame = false;
        SceneManager.LoadScene("StartScene");
    }

    public void LoadGame()
    {
        SaveManager.Instance.loadGame = true;
        SceneManager.LoadScene("StartScene");
    }
}
