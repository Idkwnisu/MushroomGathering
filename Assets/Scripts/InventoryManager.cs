using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private int money = 0;

    private static InventoryManager _instance;

    public static InventoryManager Instance { get { return _instance; } }


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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SellMushrooms(Mushroom[] mushroomsBought, int num)
    {
        for(int i = 0; i < num; i++)
        {
            money += mushroomsBought[i].value;
        }
    }

    public int getMoney()
    {
        return money;
    }

    public void spend(int moneySpent)
    {
        money -= moneySpent;
    }
}
