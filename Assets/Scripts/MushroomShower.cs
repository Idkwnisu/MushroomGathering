using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomShower : MonoBehaviour
{
    public GameObject panel;
    public Image[] images;
    public Text[] texts;

    private bool showing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMushrooms(Mushroom[] mushrooms, int num)
    {
        panel.SetActive(true);
        for(int i = 0; i < 10; i++)
        {
            if(i < num)
            {
                images[i].gameObject.SetActive(true);
                images[i].sprite = mushrooms[i].sprite;
                texts[i].text = "" + mushrooms[i].value + "G";
            }
            else
            {
                images[i].gameObject.SetActive(false);
            }
        }
        showing = true;
        Invoke("Unshow", 4.0f);
    }

    public void Unshow()
    {
        if (showing)
        {
            panel.SetActive(false);
            showing = false;
            AudioManager.Instance.PlaySound("sell");
        }
    }
}
