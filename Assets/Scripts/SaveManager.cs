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

    public bool loadGame = false;

    public Vector2[] cloudStatus;
    public int rightestCloud;

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
        cloudStatus = new Vector2[4];
    }

    public void SaveGame()
    {

        SaveState state = new SaveState();
        state.money = InventoryManager.Instance.getMoney();
        state.mushroomUpgrades = currentOwnedMushroom;
        state.backpackUpgrades = currentOwnedBackpack;
        state.timeUpgrades = currentOwnedTime;
        state.mushroomPicked = MushroomEncyclopedia.Instance.SaveMushrooms();

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
            MushroomEncyclopedia.Instance.LoadMushrooms(state.mushroomPicked);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ExistSavefile()
    {
        return (File.Exists(Application.persistentDataPath + "/savefile.save"));
    }
}
