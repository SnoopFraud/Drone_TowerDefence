using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Klik_AIFollow : MonoBehaviour
{

    public NavMeshAgent agen;
    public int Siapjalan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray klikcursor = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit titikklik;

            if(Physics.Raycast(klikcursor, out titikklik))
            {
                Siapjalan = 1;
                this.agen.SetDestination(titikklik.point);
                agen.speed = 2f;
            }
        }
        if(Siapjalan == 1)
        {
            if(agen.remainingDistance < 1) { agen.speed = 0; Siapjalan = 0; }
        }
    }
}
