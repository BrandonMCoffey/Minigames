using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public int speedMultiplier;
    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public GameObject camera;
    public float cameraBias;

    void FixedUpdate()
    {
        Vector3 moveCam = transform.position - transform.forward * 12f + transform.up * 10.0f;
        camera.transform.position = camera.transform.position * cameraBias + moveCam * (1 - cameraBias);
        camera.transform.LookAt(transform.position);

        transform.position += transform.forward * Time.deltaTime * speed;
        speed -= transform.forward.y * Time.deltaTime * speedMultiplier;
        if (speed < minSpeed)
        {
            speed = minSpeed;
        }
        else if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
    }
}
