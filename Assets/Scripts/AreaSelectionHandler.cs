using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaSelectionHandler : MonoBehaviour
{
    public GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckButtons()
    {
        List<MushroomUpgrade> areas = GenerationValuesManager.Instance.areasAvailable;
        for(int i = 0; i < buttons.Length; i++)
        {
            if(areas.Count >= i+1)
            {
                buttons[i].SetActive(true);
            }
            else
            {
                buttons[i].SetActive(false);
            }
        }
    }
}
