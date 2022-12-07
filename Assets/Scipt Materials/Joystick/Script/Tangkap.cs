using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tangkap : MonoBehaviour
{
    public int jumlahtangkapan;
    public Text teks;

    // Start is called before the first frame update
    void Start()
    {
        jumlahtangkapan = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        teks.text = "COW: " + jumlahtangkapan.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sapi")
        {
            Destroy(collision.gameObject);
            jumlahtangkapan++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sapi")
        {
            Destroy(other.gameObject);
            jumlahtangkapan++;
        }
    }

}
