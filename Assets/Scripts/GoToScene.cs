using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    public ScenesStarter scenesStarter;

    public AreaSelectionHandler areaHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            areaHandler.gameObject.SetActive(true);
            areaHandler.CheckButtons();
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            areaHandler.gameObject.SetActive(false);
        }
    }
}
