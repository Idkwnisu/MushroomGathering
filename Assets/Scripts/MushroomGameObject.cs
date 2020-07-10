using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGameObject : MonoBehaviour
{
    public Mushroom data;

    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!BackpackManager.Instance.full)
                {
                    BackpackManager.Instance.AddMushroom(data);
                    Destroy(gameObject);
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
