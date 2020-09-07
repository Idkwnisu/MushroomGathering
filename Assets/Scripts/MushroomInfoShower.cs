using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomInfoShower : MonoBehaviour
{
    public Mushroom shroom;

    public GameObject panel;
    public Image image;
    public Text mushroomName;
    public Text description;
    public Text realLifeDescription;

    public int numDescriptionUnlock = 5;
    public int numRealLifeDescriptionUnlock = 20;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatePanel()
    {
        int collected = MushroomEncyclopedia.Instance.mushroomCollection[shroom];
        image.sprite = shroom.sprite;
        mushroomName.text = shroom.mushroomName + "Collected: "+collected;
        if (collected > numDescriptionUnlock)
        {
            description.text = shroom.description;
        }
        if (collected > numRealLifeDescriptionUnlock)
        {
            realLifeDescription.text = shroom.realLifeDescription;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            active = true;
            panel.SetActive(true);
            UpdatePanel();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            active = false;
            panel.SetActive(false);
        }
    }
}
