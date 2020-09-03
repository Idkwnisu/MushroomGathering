using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{


    private static SaveManager _instance;

    public static SaveManager Instance { get { return _instance; } }

    public int currentOwnedMushroom = 0;

    public int currentOwnedBackpack = 0;

    public int currentOwnedTime = 0;


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

    public void SaveGame()
    {

        SaveState state = new SaveState();
        state.money = InventoryManager.Instance.getMoney();
        state.mushroomUpgrades = currentOwnedMushroom;
        state.backpackUpgrades = currentOwnedBackpack;
        state.timeUpgrades = currentOwnedTime;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savefile.save");
        bf.Serialize(file, state);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/savefile.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefile.save", FileMode.Open);
            SaveState state = (SaveState)bf.Deserialize(file);
            InventoryManager.Instance.setMoney(state.money);
            currentOwnedMushroom = state.mushroomUpgrades;
            currentOwnedBackpack = state.backpackUpgrades;
            currentOwnedTime = state.timeUpgrades;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
