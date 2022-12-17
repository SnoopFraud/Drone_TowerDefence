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
        objektujuan = GameObject.FindGameObjectWithTag("Tujuan");
    }
    private void Update()
    {
        targetbaru = new Vector3(objektujuan.transform.position.x, 
            objektujuan.transform.position.y,
            objektujuan.transform.position.z);

        this.agen.destination = targetbaru;
        agen.speed = 20f;
    }
}
