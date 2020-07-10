using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{

    public float speed = 1.0f;

    public Vector3 movementVector = Vector3.zero;

    public float retain = 0.5f;

    private SpriteRenderer sprite;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed),0);
    
        if(Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            anim.SetBool("Side", true);
            anim.SetBool("Front", false);
            if(movement.x > 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        else if (Mathf.Abs(movement.x) < Mathf.Abs(movement.y))
        {
            anim.SetBool("Side", false);
            anim.SetBool("Front", true);
        }

        movementVector = movementVector * retain + movement;

        transform.position += movementVector * Time.deltaTime;
    }
}
