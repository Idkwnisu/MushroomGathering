using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyMovement : MonoBehaviour
{

    public float speed = 1.0f;

    public Vector3 movementVector = Vector3.zero;

    public float retain = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Random.Range(-speed, speed), Random.Range(-speed, speed),0);

        movementVector = movementVector * retain + movement;

        transform.position += movementVector * Time.deltaTime;
    }
}
