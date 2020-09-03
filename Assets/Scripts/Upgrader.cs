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
        mushroomUpgradeText.text = mushroomUpgrades[0].upgradeName + " " + mushroomUpgrades[0].cost + "G";
        backpackUpgradeText.text = backpackUpgrades[0].upgradeName + " " + backpackUpgrades[0].cost + "G"; 
        timeUpgradeText.text = timeUpgrades[0].upgradeName + " " + timeUpgrades[0].cost + "G";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            //Save
            SaveManager.Instance.SaveGame();

            Debug.Log("Saved game");
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            SaveManager.Instance.LoadGame();

            for(int i = 0; i < SaveManager.Instance.currentOwnedMushroom; i++)
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

            for(int i = 0; i < SaveManager.Instance.currentOwnedBackpack; i++)
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

            for(int i = 0; i < SaveManager.Instance.currentOwnedTime; i++)
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

            Debug.Log("Loaded");
        }
    }

    public void BuyNextMushroom()
    {
        if(InventoryManager.Instance.getMoney() > mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].cost)
        {
            InventoryManager.Instance.spend(mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].cost);

            GenerationValuesManager.Instance.areasAvailable.Add(mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom]);

            SaveManager.Instance.currentOwnedMushroom++;
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
        if(mushroomUpgrades[SaveManager.Instance.currentOwnedMushroom].cost > InventoryManager.Instance.getMoney())
        {
            mushroomUpgradeButton.interactable = false;
        }
        else
        {
            mushroomUpgradeButton.interactable = true;
        }

        if (backpackUpgrades[SaveManager.Instance.currentOwnedBackpack].cost > InventoryManager.Instance.getMoney())
        {
            backpackUpgradeButton.interactable = false;
        }
        else
        {
            backpackUpgradeButton.interactable = true;
        }

        if (timeUpgrades[SaveManager.Instance.currentOwnedTime].cost > InventoryManager.Instance.getMoney())
        {
            timeUpgradeButton.interactable = false;
        }
        else
        {
            timeUpgradeButton.interactable = true;
        }
    }
}
