using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoToScene : MonoBehaviour
{
    public GameObject blackPanel;

    public EventSystem eventSystem;

    public Button[] buttons;

    private bool menuActive = false;
    public ScenesStarter scenesStarter;

    public AreaSelectionHandler areaHandler;

    private bool scenePanel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scenePanel && !menuActive)
        {
            if (Input.GetButtonDown("Menu"))
            {
                blackPanel.SetActive(true);
                buttons[0].Select();
                Time.timeScale = 0.0f;
                menuActive = true;
            }
        }

        if (menuActive)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                Time.timeScale = 1.0f;
                blackPanel.SetActive(false);
                menuActive = false;
                eventSystem.SetSelectedGameObject(null);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            areaHandler.gameObject.SetActive(true);
            areaHandler.CheckButtons();
            scenePanel = true;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            areaHandler.gameObject.SetActive(false);
            scenePanel = false;
        }
    }
}
