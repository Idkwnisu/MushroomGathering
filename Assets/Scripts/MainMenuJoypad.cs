using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuJoypad : MonoBehaviour
{
    public Button firstButton;
    public Button secondButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetAxis("Vertical") < 0.02f)
        {
            firstButton.Select();
        }
        else if(Input.GetAxis("Vertical") > 0.02f)
        {
            secondButton.Select();
        }
    }
}
