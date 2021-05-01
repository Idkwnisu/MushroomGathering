using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public int money = 0;
    public int mushroomUpgrades = 0;
    public int backpackUpgrades = 0;
    public int timeUpgrades = 0;

    public List<int> mushroomPicked = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
