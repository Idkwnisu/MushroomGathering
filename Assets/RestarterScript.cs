using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestarterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ReloadScene", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReloadScene()
    {
       // SceneManager.LoadScene("MushroomScene");
    }
}
