using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudMover : MonoBehaviour
{
    public Image[] clouds;
    int currentRightestImage;
    public float speed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clouds[0].GetComponent<RectTransform>().offsetMin -= new Vector2(speed, 0);
        clouds[0].GetComponent<RectTransform>().offsetMax -= new Vector2(speed, 0);
        clouds[1].GetComponent<RectTransform>().offsetMin -= new Vector2(speed, 0);
        clouds[1].GetComponent<RectTransform>().offsetMax -= new Vector2(speed, 0);
        if (clouds[currentRightestImage].GetComponent<RectTransform>().offsetMin.x <= 0)
        {
            currentRightestImage = 1 - currentRightestImage;
            clouds[currentRightestImage].GetComponent<RectTransform>().offsetMin += new Vector2(1920 * 2, 0);
            clouds[currentRightestImage].GetComponent<RectTransform>().offsetMax += new Vector2(1920 * 2, 0);
        }
    }
}
