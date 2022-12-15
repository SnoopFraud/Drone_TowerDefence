using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight3 : MonoBehaviour
{
    //Var
    Rigidbody rb;
    public AnalogFlight analog;

    float upForce;
    float speed = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        analog = GetComponent<AnalogFlight>();
    }

    private void Update()
    {
        moveUpandDown();
        forwardmovement();
        rotation();
        clampingSpeed();
        swerve();
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.up * upForce);
        rb.rotation = Quaternion.Euler(
            new Vector3(angleforward, currentrotation, tiltsideways)
            );
    }

    Vector3 speedSmooth;
    void clampingSpeed()
    {
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(
                rb.velocity.magnitude, 10f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(
                rb.velocity.magnitude, 10f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Lerp(
                rb.velocity.magnitude, 5f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.zero, ref speedSmooth, 0.95f);
        }
    }

    void moveUpandDown()
    {
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = rb.velocity;
            }
            if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Q)
                && !Input.GetKey(KeyCode.E))
            {
                rb.velocity = new Vector3(
                    rb.velocity.x, 
                    Mathf.Lerp(rb.velocity.y, 0, Time.deltaTime * 5), 
                    rb.velocity.z);
                upForce = 2.81f;
            }
            if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.Q)
                && Input.GetKey(KeyCode.E)))
            {
                rb.velocity = new Vector3(
                    rb.velocity.x,
                    Mathf.Lerp(rb.velocity.y, 0, Time.deltaTime * 5),
                    rb.velocity.z);
                upForce = 1.10f;
            }
            if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            {
                upForce = 4.10f;
            }
        }

        if(Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 1.35f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            upForce = 10 * speed;
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
            {
                upForce = 5;
            }
        } 
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            upForce = 0;
        } 
        else if(!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)
            && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f 
            && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f))
        {
            upForce = 9.81f;
        }
    }

    float speedforward = 5f;
    float angleforward = 0, anglespeed;

    void forwardmovement()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            rb.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * speedforward);
            angleforward = 
                Mathf.SmoothDamp(angleforward, 10 * Input.GetAxis("Vertical"), ref anglespeed, 0.1f);
        }
    }

    float rotationY;
    [HideInInspector] public float currentrotation;
    float rotationbykeys = 2.5f;
    float rotationYVelocity;
    void rotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rotationY -= rotationbykeys;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationY += rotationbykeys;
        }

        currentrotation = Mathf.SmoothDamp(currentrotation, rotationY, ref rotationYVelocity, 0.25f);
    }

    float SideMove = 3f;
    float tiltsideways;
    float tiltvelocity;
    void swerve()
    {
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            rb.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * SideMove);
            tiltsideways = Mathf.SmoothDamp(
                tiltsideways, -20 * Input.GetAxis("Horizontal"), ref tiltvelocity, 0.1f
                );
        }
        else
        {
            tiltsideways = Mathf.SmoothDamp(tiltsideways, 0, ref tiltvelocity, 0.1f);
        }
    }
}
