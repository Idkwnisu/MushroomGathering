using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed = 50.0f;

    public float damp = 0.8f;

    public float dampThreshold = 0.8f;

    public float terminalSpeed = 10.0f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool moving = true;
        if(BackpackManager.Instance != null)
        {
            moving = !BackpackManager.Instance.end;
        }
        if (moving)
        {
            float Hor = Input.GetAxis("Horizontal");
            float Ver = Input.GetAxis("Vertical");

            Vector2 force = new Vector2(Hor, Ver);

            if (rb2d.velocity.magnitude <= terminalSpeed)
                rb2d.AddForce(force.normalized * speed * Time.fixedDeltaTime);

            if (rb2d.velocity.magnitude >= terminalSpeed)
            {
                rb2d.velocity = rb2d.velocity.normalized * terminalSpeed;
            }


            if (Mathf.Abs(Hor) < dampThreshold || Mathf.Abs(Ver) < dampThreshold)
            {
                Vector2 dampingVector = new Vector2(1.0f, 1.0f);
                if (Mathf.Abs(Hor) < dampThreshold)
                {
                    dampingVector.x = damp;
                }
                if (Mathf.Abs(Ver) < dampThreshold)
                {
                    dampingVector.y = damp;
                }
                rb2d.velocity = rb2d.velocity * dampingVector;
            }

            if (rb2d.velocity.magnitude < 0.01f)
            {
                animator.SetBool("Side", false);
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
            }

            if (Mathf.Abs(Hor) > Mathf.Abs(Ver))
            {
                animator.SetBool("Side", true);
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
                if (Hor < 0)
                    spriteRenderer.flipX = true;
                else
                    spriteRenderer.flipX = false;
            }
            else if (Mathf.Abs(Ver) > Mathf.Abs(Hor))
            {
                if (Ver > 0)
                {
                    animator.SetBool("Up", true);
                    animator.SetBool("Down", false);
                    animator.SetBool("Side", false);
                }
                else
                {
                    animator.SetBool("Down", true);
                    animator.SetBool("Up", false);
                    animator.SetBool("Side", false);
                }
            }

        }
    }
}
