using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvasManager : MonoBehaviour
{
    public Image blueBackground; //fade
    public Image[] clouds; //fade
    public Image title; //go up
    public Image forest; //go down
    public GameObject[] buttons; //go down

    public float movementSpeed = 0.5f;
    public float movementAcceleration = 0.03f;
    public float movementTime = 1.0f;
    public float interval = 0.1f;

    public float fadeSpeed = 0.5f;
    public float fadeAcceleration = 0.03f;
    public float fadeTime = 1.0f;
    public float fadeStart = 0.8f;
    public float intervalFade = 0.1f;

    Vector3 titlePosition;
    Vector3 forestPosition;
    Vector3[] buttonsPositions;

    Coroutine fade;
    Coroutine move;

    private void Awake()
    {
        buttonsPositions = new Vector3[buttons.Length];
        titlePosition = title.transform.position;
        forestPosition = forest.transform.position;
        for(int i = 0; i < buttons.Length; i++)
        {
            buttonsPositions[i] = buttons[i].transform.position;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        move = StartCoroutine(OpenAnimation());
        Invoke("FadeStarter", fadeStart);
    }

    // Update is called once per frame
    void Update()
    {
        if(fade == null && move == null)
        {
            Destroy(this);
        }
    }

    public void FadeStarter()
    {
        fade = StartCoroutine(FadeAnimation());
    }

    IEnumerator OpenAnimation()
    {
        float time = 0.0f;
        while (time < movementTime)
        {
            title.transform.position += Vector3.up * movementSpeed;
            forest.transform.position -= Vector3.up * movementSpeed;
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.position -= Vector3.up * movementSpeed;
            }
            time += interval;
            movementSpeed += movementAcceleration;
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator FadeAnimation()
    {
        float alpha = 1.0f;
        while (alpha > 0)
        {
            alpha -= fadeSpeed;
            if(alpha < 0.0f)
            {
                alpha = 0.0f;
            }
            blueBackground.color = new Color(blueBackground.color.r, blueBackground.color.g, blueBackground.color.b, alpha);
            for(int i = 0; i < clouds.Length; i++)
            {
                clouds[i].color = new Color(clouds[i].color.r, clouds[i].color.g, clouds[i].color.b, alpha);
            }
            fadeSpeed += fadeAcceleration;
            yield return new WaitForSeconds(intervalFade);
        }
    }

    public void Reset()
    {
        blueBackground.color = Color.white;
        for(int i = 0; i < clouds.Length; i++)
        {
            clouds[i].color = Color.white;
        }
        titlePosition = title.transform.position;
        forestPosition = forest.transform.position;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonsPositions[i] = buttons[i].transform.position;
        }
    }
}
