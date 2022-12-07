using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour
{
    GameObject objektujuan;
    Vector3 targetbaru;
    NavMeshAgent agen;

    private void Start()
    {
        agen = this.GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        objektujuan = GameObject.FindGameObjectWithTag("Tujuan");
        targetbaru = new Vector3(objektujuan.transform.position.x, 
            objektujuan.transform.position.y,
            objektujuan.transform.position.z);

        this.agen.destination = targetbaru;
        agen.speed = 3f;

        if(this.agen.remainingDistance < 0.5) { Destroy(objektujuan); }
    }
}
