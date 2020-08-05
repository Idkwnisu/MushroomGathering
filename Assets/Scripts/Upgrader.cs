﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrader : MonoBehaviour
{
    public Text mushroomUpgradeText;
    public Text backpackUpgradeText;
    public Text timeUpgradeText;

    public MushroomUpgrade[] mushroomUpgrades;
    public BackpackUpgrade[] backpackUpgrades;
    public TimeUpgrade[] timeUpgrades;

    private int currentOwnedMushroom = 0;

    private bool fullMushroomBought = false;

    private int currentOwnedBackpack = 0;

    private int currentOwnedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Take prefs here
        mushroomUpgradeText.text = mushroomUpgrades[0].upgradeName;
        backpackUpgradeText.text = backpackUpgrades[0].upgradeName;
        timeUpgradeText.text = timeUpgrades[0].upgradeName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyNextMushroom()
    {
        if(InventoryManager.Instance.getMoney() > mushroomUpgrades[currentOwnedMushroom].cost)
        {
            InventoryManager.Instance.spend(mushroomUpgrades[currentOwnedMushroom].cost);

            GenerationValuesManager.Instance.areasAvailable.Add(mushroomUpgrades[currentOwnedMushroom]);

            currentOwnedMushroom++;
            if (currentOwnedMushroom >= mushroomUpgrades.Length)
            {
                mushroomUpgradeText.GetComponentInParent<Button>().interactable = false;
                mushroomUpgradeText.text = "-";
            }
            else
            {
                mushroomUpgradeText.text = mushroomUpgrades[currentOwnedMushroom].upgradeName;
            }
        }
    }

    public void BuyNextBackpack()
    {
        if (InventoryManager.Instance.getMoney() > backpackUpgrades[currentOwnedMushroom].cost)
        {
            InventoryManager.Instance.spend(backpackUpgrades[currentOwnedMushroom].cost);

            GenerationValuesManager.Instance.backpackSize = backpackUpgrades[currentOwnedMushroom].newSize;

            currentOwnedBackpack++;
            if (currentOwnedBackpack >= backpackUpgrades.Length)
            {
                backpackUpgradeText.GetComponentInParent<Button>().interactable = false;
                backpackUpgradeText.text = "-";
            }
            else
            {
                backpackUpgradeText.text = backpackUpgrades[currentOwnedBackpack].upgradeName;
            }
        }
    }

    public void BuyNextTime()
    {
        if (InventoryManager.Instance.getMoney() > timeUpgrades[currentOwnedTime].cost)
        {
            InventoryManager.Instance.spend(timeUpgrades[currentOwnedTime].cost);

            GenerationValuesManager.Instance.duration = timeUpgrades[currentOwnedTime].newSeconds;

            currentOwnedTime++;
            if (currentOwnedTime >= timeUpgrades.Length)
            {
                timeUpgradeText.GetComponentInParent<Button>().interactable = false;
                timeUpgradeText.text = "-";
            }
            else
            {
                timeUpgradeText.text = timeUpgrades[currentOwnedTime].upgradeName;
            }
        }
    }
}
