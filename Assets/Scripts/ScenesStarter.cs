using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMushroomScene()
    {
        SceneManager.LoadScene("MushroomScene");
    }

    public void StartWithTime(bool night)
    {
        GenerationValuesManager.Instance.night = night;
        StartMushroomScene();
    }
}
