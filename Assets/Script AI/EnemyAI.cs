using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Var
    public int 
        currenthealth, 
        maxhealth;

    public GameObject Goal;
    NavMeshAgent agent;
    public float jarak;
    public int cariTarget;

    // Start is called before the first frame update
    void Start()
    {
        Goal = GameObject.FindGameObjectWithTag("Tujuan");
        agent = this.GetComponent<NavMeshAgent>();
        this.agent.speed = 10f;
        cariTarget = 1;
        currenthealth = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        jarak = Vector3.Distance(this.transform.position,
            this.Goal.transform.position);

        if (cariTarget == 1)
        {
            agent.destination = new Vector3(
            this.Goal.transform.position.x,
            this.Goal.transform.position.y,
            this.Goal.transform.position.z
            );
            if (jarak < 1) { cariTarget = 0; this.agent.speed = 0f; }
        }
        if (jarak > 1) { cariTarget = 1; this.agent.speed = 10f; }
    }

    public void takeDamage(int damage)
    {
        currenthealth -= damage;

        if(currenthealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Enemy die");
        gameObject.SetActive(false);
        this.gameObject.tag = "Untagged";
    }
}
