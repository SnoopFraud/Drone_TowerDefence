using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kendali : MonoBehaviour
{
    int statmaju, statmundur, statkanan, statkiri;
    public GameObject ObjekGerak;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(statmaju == 1)
        {
            float gerak = Time.deltaTime * 10f;
            ObjekGerak.transform.Translate(0, 0, gerak);
        }
        if(statmundur == 1)
        {
            float gerak = Time.deltaTime * 10f;
            ObjekGerak.transform.Translate(0, 0, -gerak);
        }
        if(statkanan == 1)
        {
            float gerak = Time.deltaTime * 10f;
            ObjekGerak.transform.Translate(gerak, 0, 0);
        }
        if(statkiri == 1)
        {
            float gerak = Time.deltaTime * 10f;
            ObjekGerak.transform.Translate(-gerak, 0, 0);
        }
        
    }

    //Maju
    public void MajudiTekan()
    {
        statmaju = 1;
    }

    public void lepasmaju()
    {
        statmaju = 0;
    }
    //Mundur
    public void MundurdiTekan()
    {
        statmundur = 1;
    }

    public void lepasmundur()
    {
        statmundur = 0;
    }
    //Kanan
    public void KanandiTekan()
    {
        statkanan = 1;
    }

    public void lepaskanan()
    {
        statkanan = 0;
    }
    //Kiri
    public void KiridiTekan()
    {
        statkiri = 1;
    }

    public void lepaskiri()
    {
        statkiri = 0;
    }
}
