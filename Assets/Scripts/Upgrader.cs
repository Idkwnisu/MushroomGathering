using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrader : MonoBehaviour
{
    public Text mushroomUpgradeText;
    public Text backpackUpgradeText;
    public Text timeUpgradeText;

    public Button mushroomUpgradeButton;
    public Button backpackUpgradeButton;
    public Button timeUpgradeButton;

    public MushroomUpgrade[] mushroomUpgrades;
    public BackpackUpgrade[] backpackUpgrades;
    public TimeUpgrade[] timeUpgrades;


    private bool fullMushroomBought = false;

    // Start is called before the first frame update
    void Start()
    {
        //Take prefs here
        //check here if it loads badly something
      

        mushroomUpgradeText.text = mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].upgradeName + " " + mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].cost + "G";
        backpackUpgradeText.text = backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].upgradeName + " " + backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].cost + "G"; 
        timeUpgradeText.text = timeUpgrades[SaveManager.Instance.currentOwnedTime].upgradeName + " " + timeUpgrades[SaveManager.Instance.currentOwnedTime].cost + "G";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Save();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        //Save
        SaveManager.Instance.SaveGame();

        Debug.Log("Saved game");
    }

    public void Load()
    {
        SaveManager.Instance.LoadGame();

        ReadSaveManager();

        Debug.Log("Loaded");
    }

    public void ReadSaveManager()
    {
        for (int i = 0; i < SaveManager.Instance.currentOwnedMushroom; i++)
        {
            GenerationValuesManager.Instance.areasAvailable.Add(mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom]);

            if (SaveManager.Instance.currentOwnedMushroom >= mushroomUpgrades.Length)
            {
                mushroomUpgradeText.GetComponentInParent<Button>().interactable = false;
                mushroomUpgradeText.text = "-";
            }
            else
            {
                mushroomUpgradeText.text = mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].upgradeName + " " + mushroomUpgrades[0].cost + "G";
            }
        }

        for (int i = 0; i < SaveManager.Instance.currentOwnedBackpack; i++)
        {
            InventoryManager.Instance.spend(backpackUpgrades[SaveManager.Instance.currentOwnedMushroom].cost);

            GenerationValuesManager.Instance.backpackSize = backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].newSize;

            if (SaveManager.Instance.currentOwnedBackpack >= backpackUpgrades.Length)
            {
                backpackUpgradeText.GetComponentInParent<Button>().interactable = false;
                backpackUpgradeText.text = "-";
            }
            else
            {
                backpackUpgradeText.text = backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].upgradeName + " " + backpackUpgrades[0].cost + "G";
            }
        }

        for (int i = 0; i < SaveManager.Instance.currentOwnedTime; i++)
        {
            InventoryManager.Instance.spend(timeUpgrades[SaveManager.Instance.currentOwnedTime].cost);

            GenerationValuesManager.Instance.duration = timeUpgrades[SaveManager.Instance.currentOwnedTime].newSeconds;

            if (SaveManager.Instance.currentOwnedTime >= timeUpgrades.Length)
            {
                timeUpgradeText.GetComponentInParent<Button>().interactable = false;
                timeUpgradeText.text = "-";
            }
            else
            {
                timeUpgradeText.text = timeUpgrades[SaveManager.Instance.currentOwnedTime].upgradeName + " " + timeUpgrades[0].cost + "G";
            }
        }
    }

    public void BuyNextMushroom()
    {
        if(InventoryManager.Instance.getMoney() > mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].cost)
        {
            InventoryManager.Instance.spend(mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].cost);

            GenerationValuesManager.Instance.areasAvailable.Add(mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom]);

            SaveManager.Instance.currentOwnedMushroom++;
            MushroomEncyclopediaManager.Instance.UpdateRooms();
            if (SaveManager.Instance.currentOwnedMushroom >= mushroomUpgrades.Length)
            {
                mushroomUpgradeText.GetComponentInParent<Button>().interactable = false;
                mushroomUpgradeText.text = "-";
            }
            else
            {
                mushroomUpgradeText.text = mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].upgradeName + " " + mushroomUpgrades[0].cost + "G";
            }
        }
    }

    public void BuyNextBackpack()
    {
        if (InventoryManager.Instance.getMoney() > backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].cost)
        {
            InventoryManager.Instance.spend(backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].cost);

            GenerationValuesManager.Instance.backpackSize = backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].newSize;

            SaveManager.Instance.currentOwnedBackpack++;
            if (SaveManager.Instance.currentOwnedBackpack >= backpackUpgrades.Length)
            {
                backpackUpgradeText.GetComponentInParent<Button>().interactable = false;
                backpackUpgradeText.text = "-";
            }
            else
            {
                backpackUpgradeText.text = backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].upgradeName + " " + backpackUpgrades[0].cost + "G";
            }
        }
    }

    public void BuyNextTime()
    {
        if (InventoryManager.Instance.getMoney() > timeUpgrades[SaveManager.Instance.currentOwnedTime].cost)
        {
            InventoryManager.Instance.spend(timeUpgrades[SaveManager.Instance.currentOwnedTime].cost);

            GenerationValuesManager.Instance.duration = timeUpgrades[SaveManager.Instance.currentOwnedTime].newSeconds;

            SaveManager.Instance.currentOwnedTime++;
            if (SaveManager.Instance.currentOwnedTime >= timeUpgrades.Length)
            {
                timeUpgradeText.GetComponentInParent<Button>().interactable = false;
                timeUpgradeText.text = "-";
            }
            else
            {
                timeUpgradeText.text = timeUpgrades[SaveManager.Instance.currentOwnedTime].upgradeName + " " + timeUpgrades[0].cost + "G";
            }
        }
    }

    public void CheckButtons()
    {
        //controlla che ci siano ancora upgrade
        if(mushroomUpgrades.Length < SaveManager.Instance.currentOwnedMushroom)
        {
            if (mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].cost > InventoryManager.Instance.getMoney())
            {
                mushroomUpgradeButton.interactable = false;
            }
            else
            {
                mushroomUpgradeButton.interactable = true;
            }
        }
        else
        {
            mushroomUpgradeButton.interactable = false;
        }


        if (backpackUpgrades.Length < SaveManager.Instance.currentOwnedBackpack)
        {
            if (backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].cost > InventoryManager.Instance.getMoney())
            {
                backpackUpgradeButton.interactable = false;
            }
            else
            {
                backpackUpgradeButton.interactable = true;
            }
        }
        else
        {
            backpackUpgradeButton.interactable = false;
        }

        if (timeUpgrades.Length < SaveManager.Instance.currentOwnedTime)
        {

            if (timeUpgrades[SaveManager.Instance.currentOwnedTime].cost > InventoryManager.Instance.getMoney())
            {
                timeUpgradeButton.interactable = false;
            }
            else
            {
                timeUpgradeButton.interactable = true;
            }
        }
        else
        {
            timeUpgradeButton.interactable = false;
        }
    }
}
