using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFunctions : MonoBehaviour
{
    private StartCanvasManager canvasManager;
    // Start is called before the first frame update
    void Start()
    {
        canvasManager = FindObjectOfType<StartCanvasManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
