using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGameObject : MonoBehaviour
{
    public Mushroom data;

    public bool active = false;
    public float gatheringSpeed = 10.0f;

    private Transform player;
    
    private bool gathering = false;
    private float gatheringAmount = 0.0f;

    private Vector3 positionStart;
    private Vector3 scaleStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gathering)
        {
            gatheringAmount += gatheringSpeed * Time.deltaTime;
            if(gatheringAmount >= 1.0f)
            {
                BackpackManager.Instance.AddMushroom(data);
                Destroy(gameObject);
            }
            else
            {
                transform.position = positionStart + gatheringAmount * (player.position - transform.position);
                transform.localScale = scaleStart - scaleStart * gatheringAmount;
            }
        }

        if(active)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
            {
                if (!BackpackManager.Instance.full)
                {
                    
                    gathering = true;
                    AudioManager.Instance.PlaySound("gather"); // a bit coupled, but we have so few istances that it would be just a time loss to decouple it
                    BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
                    for(int i = 0; i < colliders.Length; i++)
                    {
                        colliders[i].enabled = false;
                    }
                    positionStart = transform.position;
                    scaleStart = transform.localScale;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            active = true;
            BackpackManager.Instance.changeTake(true);
            player = col.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            active = false;
            BackpackManager.Instance.changeTake(false);
        }
    }
}
