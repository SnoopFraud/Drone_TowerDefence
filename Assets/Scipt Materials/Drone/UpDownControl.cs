using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpDownControl : MonoBehaviour
{
    //var
    //Drone
    public Rigidbody rb;
    float
        upForce,
        speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.up * upForce);
    }
    private void Update()
    {
        
    }

    public void UpMovement()
    {
        upForce = 10 * speed;
    }

    public void DownMovement()
    {
        upForce = 10 * -speed;
    }
}
