using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationValuesManager : MonoBehaviour
{
    public int size = 300;

    public int nrSteps = 5000;

    public int nrInits = 1;

    public int smooth = 6;

    public int smoothFactor = 4;

    public float treeGrassLikeness = 0.05f;
    public float treePathLikeness = 0.01f;

    public float decorationLikeness = 0.1f;

    public float lamppostLikeness = 0.01f;

    public float mushroomLikeness = 0.3f;

    public GameObject[] mushrooms;

    public float startPercentage = 0.05f;
    public float percentageIncrement = 0.01f;

    public bool night = false;


    private static GenerationValuesManager _instance;

    public static GenerationValuesManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            DontDestroyOnLoad(this);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
