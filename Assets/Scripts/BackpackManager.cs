using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BackpackManager : MonoBehaviour
{

    public Text sizeText;

    public Text timeText;

    public GameObject takeText;

    public int backpackSize = 5;

    public int seconds = 120;

    private int mushroomNr = 0;

    public GameObject endPanel;
    public Text endText;
    [HideInInspector]
    public bool end = false;

    public float changeDelay = 0.3f;

    private Mushroom[] mushroomsTaken;
    private static BackpackManager _instance;

    public static BackpackManager Instance { get { return _instance; } }

    public bool full { get => (mushroomNr == backpackSize); }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        backpackSize = GenerationValuesManager.Instance.backpackSize;
        seconds = GenerationValuesManager.Instance.duration;
        resetBackpack(backpackSize);
        StartCoroutine("TimeCoroutine");
        UpdateTimeText();
    }

    public void resetBackpack(int size)
    {
        backpackSize = size;

        mushroomNr = 0;

        mushroomsTaken = new Mushroom[size];

        UpdateText();
    }

    public void UpdateText()
    {

        sizeText.text = mushroomNr + "/" + backpackSize;
    }

    public void UpdateTimeText()
    {
        timeText.text = Mathf.Floor(seconds/60) + ":" + (seconds%60).ToString("00");
    }

    public void AddMushroom(Mushroom newShroom)
    {
        mushroomsTaken[mushroomNr] = newShroom;
        mushroomNr++;
        UpdateText();

        if(mushroomNr >= mushroomsTaken.Length)
        {
            endPanel.SetActive(true);
            endText.text = "Backpack is full";
            end = true;
            AudioManager.Instance.PlaySound("end");

            Invoke("ChangeScene", changeDelay);
        }
    }

    public void changeTake(bool take)
    {
        takeText.SetActive(take);
    }

    IEnumerator TimeCoroutine()
    {
        while (seconds > 0)
        {
            if (!MenuHandler.Instance.paused)
                seconds--;
            UpdateTimeText();
            yield return new WaitForSecondsRealtime(1);
        }

        //Time is up
        endPanel.SetActive(true);
        endText.text = "Time Over";
        end = true;
        AudioManager.Instance.PlaySound("end");
        Invoke("ChangeScene", changeDelay);
    }

   public void ChangeScene()
    {
        InventoryManager.Instance.SellMushrooms(mushroomsTaken, mushroomNr);
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene("StartScene");
    }
}
