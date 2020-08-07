using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    private int money = 0;
    public int startMoney = 200;
    public Text moneyText;

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
        money = startMoney;
        moneyText.text = money + "G";
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

        moneyText.text = money + "G";
    }

    public int getMoney()
    {
        return money;
    }

    public void spend(int moneySpent)
    {
        money -= moneySpent;
        moneyText.text = money + "G";
    }
}
