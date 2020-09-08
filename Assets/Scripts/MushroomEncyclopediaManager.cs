using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEncyclopediaManager : MonoBehaviour
{
    private static MushroomEncyclopediaManager _instance;

    public static MushroomEncyclopediaManager Instance { get { return _instance; } }

    public GameObject[] indicators;

    public GameObject[] roomBlockers;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSprites()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            if (MushroomEncyclopedia.Instance.mushroomCollection[MushroomEncyclopedia.Instance.mushrooms[i]] > 0)
            {
                indicators[i].SetActive(true);
            }
            else
            {
                indicators[i].SetActive(false);
            }
        }
    }

    public void UpdateRooms()
    {
        Debug.Log("Update rooms");
        for(int i = 0; i < SaveManager.Instance.currentOwnedMushroom; i++)
        {
            if(i < roomBlockers.Length)
            {
                roomBlockers[i].SetActive(false);
            }
        }
    }
}
