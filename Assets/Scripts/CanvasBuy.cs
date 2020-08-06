using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBuy : MonoBehaviour
{
    public GameObject canvasUpgrade;
    private Upgrader upgrader;
    // Start is called before the first frame update
    void Start()
    {
        upgrader = GetComponent<Upgrader>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            canvasUpgrade.SetActive(true);
            upgrader.CheckButtons();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            canvasUpgrade.SetActive(false);
            upgrader.CheckButtons();

        }
    }
}
