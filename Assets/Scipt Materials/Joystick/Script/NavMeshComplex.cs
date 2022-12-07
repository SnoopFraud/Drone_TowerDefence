using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshComplex : MonoBehaviour
{
    Vector3 targetterdekat;
    public float jarak;
    public int caritarget;
    public float rangecari;
    public float batasdekat;
    NavMeshAgent agen;
    int posisitarget;
    GameObject[] objektujuan;

    private void Start()
    {
        agen = GetComponent<NavMeshAgent>();
        caritarget = 1;
        rangecari = 10f;
        this.agen.speed = 0f;
        batasdekat = 0.5f;
    }

    private void Update()
    {
        if(caritarget == 1)
        {
            caritargetdekat();
        }
        else
        {
            if(objektujuan[posisitarget] == null) { caritarget = 1;}
            else
            {
                if(this.agen.remainingDistance < batasdekat)
                {
                    Destroy(objektujuan[posisitarget].gameObject);
                    caritarget = 1;
                    this.agen.speed = 0f;
                }
            }
        }
    }

    void caritargetdekat()
    {
        if(caritarget == 1)
        {
            objektujuan = GameObject.FindGameObjectsWithTag("Tujuan");
            if(GameObject.FindGameObjectsWithTag("Tujuan") != null)
            {
                targetterdekat = new Vector3(1000, 1000, 1000);
                for(int i = 0; i < objektujuan.Length; i++)
                {
                    Vector3 jarakdaritarget 
                        = this.transform.position - objektujuan[i].transform.position;

                    if(jarakdaritarget.magnitude < targetterdekat.magnitude)
                    {
                        targetterdekat = jarakdaritarget;
                        posisitarget = i;
                        caritarget = 0;
                        jarak = Vector3.Distance(this.transform.position, objektujuan[i].transform.position);
                    }
                }
            }
            else
            {
                caritarget = 1;
            }
        }

        if(caritarget == 0 && jarak <= rangecari)
        {
            this.agen.destination = objektujuan[posisitarget].transform.position;
            this.agen.speed = 1f;
        }
        else
        {
            caritarget = 1;
        }
        
    }
}
