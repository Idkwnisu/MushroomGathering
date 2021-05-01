using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasBuy : MonoBehaviour
{
    public GameObject canvasUpgrade;
    public GameObject blackPanel;
    private Upgrader upgrader;

    public EventSystem eventSystem;

    public Button[] buttons;

    private bool menuActive = false;
    private bool upgraderPanel = false;
    // Start is called before the first frame update
    void Start()
    {
        upgrader = GetComponent<Upgrader>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(upgraderPanel && !menuActive)
        {
            if(Input.GetButtonDown("Menu"))
            {
                blackPanel.SetActive(true);
                buttons[0].Select();
                Time.timeScale = 0.0f;
                menuActive = true;
            }
        }

        if(menuActive)
        {
            if(Input.GetButtonDown("Fire3"))
            {
                Time.timeScale = 1.0f;
                blackPanel.SetActive(false);
                menuActive = false;
                eventSystem.SetSelectedGameObject(null);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            canvasUpgrade.SetActive(true);
            upgrader.CheckButtons();
            upgraderPanel = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            canvasUpgrade.SetActive(false);
            upgrader.CheckButtons();
            upgraderPanel = false;
            blackPanel.SetActive(false);
           
        }
    }
}
