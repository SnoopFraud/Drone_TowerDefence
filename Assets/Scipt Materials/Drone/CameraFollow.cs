using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //var
    public Transform Drone;

    private Vector3 camerafollowspeed;
    public Vector3 behindPos = new Vector3(0, 2, -4);
    public float 
        pos_lerp = 0.2f,
        rotate_lerp = 0.1f;

    private void Awake()
    {
        Drone = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    private void FixedUpdate()
    {
        //Camera Follow
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            Drone.transform.TransformPoint(behindPos) + Vector3.up,
            ref camerafollowspeed,
            pos_lerp);
        //Camera Rotate
        transform.rotation = Quaternion.Lerp(transform.rotation, Drone.rotation, rotate_lerp);
    }
}
