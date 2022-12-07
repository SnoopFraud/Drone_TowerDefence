using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ViewDisMax : MonoBehaviour
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

    public string Statepemain = "DIAM";
    public string StateLama;
    // Start is called before the first frame update
    void Start()
    {
        jarakminketarget = 2;
        VisDistance = 4f;
        VisAngle = 120;
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
                    if(Statepemain != "DIAM")
                    {
                        Statepemain = "DIAM";
                    }
                }
            }
            
        }
        else
        {
            if (Statepemain != "DIAM")
            {
                Statepemain = "DIAM";
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
        if (Statepemain == "DIAM")
        {
            if (StateLama != "DIAM")
            {
                StateLama = "DIAM";
                this.agen.speed = 0f;
                this.agen.angularSpeed = 0f;
                this.agen.acceleration = 0f;
            }
            this.agen.SetDestination(this.target.transform.position);
        }
    }
}
