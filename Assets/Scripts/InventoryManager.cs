using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{

    private int money = 0;
    public int startMoney = 200;
    public Text moneyText;

    public MushroomShower shower;

    private static InventoryManager _instance;

    private Mushroom[] toSell;
    private int numToSell;
    private bool selling = false;

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
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("enable");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (selling)
        {
            MushroomEncyclopedia.Instance.CollectMushrooms(toSell,numToSell);

            for (int i = 0; i < numToSell; i++)
            {
                money += toSell[i].value;
            }

            moneyText.text = money + "G";
            shower = FindObjectOfType<MushroomShower>();
            shower.ShowMushrooms(toSell, numToSell);
        }
        Debug.Log("Loaded");
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
        toSell = mushroomsBought;
        numToSell = num;
        selling = true;
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

    public void setMoney(int newMoney)
    {
        money = newMoney;
        moneyText.text = money + "G";
    }
}
