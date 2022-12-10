using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    //var
    public Rigidbody rb;

    float up_down_axis, forward_backward_axis, right_left_axis;
    float forward_backward_angle = 0, right_left_angle = 0;

    [SerializeField]
    float speed = 1.3f, angle = 25;

    public bool isOnGround = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        dronecontroller();
        transform.localEulerAngles = Vector3.back * right_left_angle + Vector3.right * forward_backward_angle;
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(right_left_axis, up_down_axis, forward_backward_axis);
    }

    void dronecontroller()
    {
        //Control here
        if (Input.GetKey(KeyCode.Mouse0))
        {
            up_down_axis = 10 * speed;
            isOnGround = false;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            up_down_axis = 0;
        }
        else
        {
            up_down_axis = 9.81f;
        }

        //WASD Control
        //Maju Mundur
        if (Input.GetKey(KeyCode.W))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, angle, Time.deltaTime);
            forward_backward_axis = speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, -angle, Time.deltaTime);
            forward_backward_axis = -speed;
        }
        else
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, 0, Time.deltaTime);
            forward_backward_axis = 0;
        }
        //Kanan kiri
        if (Input.GetKey(KeyCode.D))
        {
            right_left_angle = Mathf.Lerp(right_left_angle, angle, Time.deltaTime);
            right_left_axis = speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            right_left_angle = Mathf.Lerp(right_left_angle, -angle, Time.deltaTime);
            right_left_axis = -speed;
        }
        else
        {
            right_left_angle = Mathf.Lerp(right_left_angle, 0, Time.deltaTime);
            right_left_axis = 0;
        }
        //Maju ke kanan
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, angle, Time.deltaTime);
            right_left_angle = Mathf.Lerp(right_left_angle, angle, Time.deltaTime);
            forward_backward_axis = 0.5f * speed;
            right_left_axis = 0.5f * speed;
        }
        //Maju ke kiri
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, angle, Time.deltaTime);
            right_left_angle = Mathf.Lerp(right_left_angle, -angle, Time.deltaTime);
            forward_backward_axis = 0.5f * speed;
            right_left_axis = -0.5f * speed;
        }
        //Mundur ke kanan
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, -angle, Time.deltaTime);
            right_left_angle = Mathf.Lerp(right_left_angle, angle, Time.deltaTime);
            forward_backward_axis = -0.5f * speed;
            right_left_axis = 0.5f * speed;
        }
        //Mundur ke kiri
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            forward_backward_angle = Mathf.Lerp(forward_backward_angle, -angle, Time.deltaTime);
            right_left_angle = Mathf.Lerp(right_left_angle, -angle, Time.deltaTime);
            forward_backward_axis = -0.5f * speed;
            right_left_axis = -0.5f * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }
}
