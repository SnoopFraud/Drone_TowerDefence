using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    //var
    private Rigidbody rb;
    float up_down_axis, forward_backward_axis, right_left_axis;
    float forward_backward_angle, right_left_angle;

    [SerializeField]
    float speed, angle;

    void dronecontroller()
    {
        //Control here
    }
}
