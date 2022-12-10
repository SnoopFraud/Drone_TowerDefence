using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Drone;

    private void Awake()
    {
        Drone = GameObject.FindGameObjectWithTag("Player").transform;
    }

    Vector3 velocitycamera;
    public Vector3 behindposition = new Vector3(0, 2, -4);
    public float angle;
    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            Drone.transform.TransformPoint(behindposition) + Vector3.up * Input.GetAxis("Vertical"), 
            ref velocitycamera, 0.1f
            );
        transform.rotation = Quaternion.Euler(
            new Vector3(angle, Drone.GetComponent<Flight2>().currentrotation, 0)
            );
    }
}
