using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavKlik : MonoBehaviour
{
    public NavMeshAgent agen;
    public int aktif;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && aktif == 2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("plane"))
                {
                    this.agen.SetDestination(hit.point);
                }
                this.aktif = 0;
                this.GetComponent<Renderer>().material.color = new Color(0, 0, 255);

            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            this.aktif = 0;
            this.GetComponent<Renderer>().material.color = new Color(0, 0, 255);

        }
    }

    private void OnMouseDown()
    {
        if(this.aktif == 0) { 
            this.aktif = 1;
            this.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        } else { 
            this.aktif = 0;
            this.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
        }
    }

    private void OnMouseUp()
    {
        if(aktif == 1) { aktif = 2; }
    }
}
