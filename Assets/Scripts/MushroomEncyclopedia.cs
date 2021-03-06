﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEncyclopedia : MonoBehaviour
{
    private static MushroomEncyclopedia _instance;

    public static MushroomEncyclopedia Instance { get { return _instance; } }

    public Mushroom[] mushrooms;
    public Dictionary<Mushroom, int> mushroomCollection;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (mushroomCollection == null)
        {
            mushroomCollection = new Dictionary<Mushroom, int>();
            for (int i = 0; i < mushrooms.Length; i++)
            {
                mushroomCollection.Add(mushrooms[i], 0);
            }
        }
    }

    public void LoadMushrooms(List<int> mushroomPicked)
    {

        mushroomCollection = new Dictionary<Mushroom, int>();

        for (int i = 0; i < mushroomPicked.Count; i++)
        {
            mushroomCollection[mushrooms[i]] = mushroomPicked[i];
        }
        MushroomEncyclopediaManager.Instance.UpdateSprites();
        MushroomEncyclopediaManager.Instance.UpdateRooms();
    }

    public List<int> SaveMushrooms()
    {
        List<int> toReturn = new List<int>();
        for(int i = 0; i < mushrooms.Length; i++)
        {
            toReturn.Add(mushroomCollection[mushrooms[i]]);
        }
        return toReturn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectMushrooms(Mushroom[] toCollect, int num)
    {
        for(int i = 0; i < num; i++)
        {
            mushroomCollection[toCollect[i]]++;
        }
        MushroomEncyclopediaManager.Instance.UpdateSprites();
        MushroomEncyclopediaManager.Instance.UpdateRooms();
    }

    
}
