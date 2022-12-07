using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FOVPatrol : MonoBehaviour
{
    public NavMeshAgent agen;
    public Camera cam;
    public float jarakminketarget;
    public Transform target;

    public Vector3 jarakXYZketarget;
    public float Vektorketarget;
    public float jarakvektorketarget;

    public float VisDistance;
    public float VisAngle;

    public float jaraktangkap = 3.0f;

    public string Statepemain = "PATROL";
    public string StateLama;

    public float Jitter;

    // Start is called before the first frame update
    void Start()
    {
        //jarakminketarget = 2;
        //VisDistance = 4f;
        //VisAngle = 120;
    }

    // Update is called once per frame
    void Update()
    {
        jarakXYZketarget = target.position - this.transform.position;
        Vektorketarget = Vector3.Angle(jarakXYZketarget, this.transform.forward);
        jarakvektorketarget = jarakXYZketarget.magnitude;

        if(jarakvektorketarget < VisDistance && Vektorketarget < VisAngle)
        {
            RaycastHit hit;
            if(Physics.Raycast(this.transform.position, jarakXYZketarget, out hit))
            {
                //Deteksi siapa yang ditabrak
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.DrawRay(this.transform.position, jarakXYZketarget, Color.green);

                    if(Statepemain != "KEJAR")
                    {
                        Statepemain = "KEJAR";
                    }
                }
                else
                {
                    if(Statepemain != "PATROL")
                    {
                        Statepemain = "PATROL";
                    }
                }
            }
            
        }
        else
        {
            if (Statepemain != "PATROL")
            {
                Statepemain = "PATROL";
            }
        }
        if(Statepemain == "KEJAR")
        {
            if(StateLama != "KEJAR")
            {
                StateLama = "KEJAR";
                this.agen.speed = 3.5f;
                this.agen.angularSpeed = 250f;
                this.agen.acceleration = 8f;
                
            }
            this.agen.SetDestination(this.target.transform.position);
        }

        //Copas State atas
        if (Statepemain == "PATROL")
        {
            if (StateLama != "PATROL")
            {
                StateLama = "PATROL";
                this.agen.speed = 2f;
                this.agen.angularSpeed = 100f;
                this.agen.acceleration = 5f;
                Jalan();
            }
            if(this.agen.remainingDistance < 2)
            {
                Jalan();
            }
            //this.agen.SetDestination(this.target.transform.position);
        }
    }

    void Jalan()
    {
        Vector3 wandertarget = Vector3.zero;
        float wanderarea = 6f;
        float wanderdistance = 9f;

        wandertarget += new Vector3(//x, y, z
            Random.Range(-1f, 1f) * Jitter,
            0,
            Random.Range(-1f, 1f) * Jitter
            );
        wandertarget.Normalize();
        wandertarget *= wanderarea;
        //Sudah memiliki posisi acak
        //Selanjutnya geser kedepan
        
        Vector3 targetlokal = wandertarget + new Vector3(0, 0, wanderdistance); //Sekitar karakter
        Vector3 targetworld = this.gameObject.transform.InverseTransformVector(targetlokal);
        this.agen.SetDestination(targetworld);
    }
}
