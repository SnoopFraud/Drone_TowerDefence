using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    public int 
        health, 
        maxhealth;
    GameManager GM;
    string gmtag;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        health = maxhealth;
    }

    private void Update()
    {
        if(health <= 0)
        {
            GM.isGameOver = true;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 50;   
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health -= 50;
            other.gameObject.SetActive(false);
        }
    }
}
