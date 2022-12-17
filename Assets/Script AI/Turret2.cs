using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : MonoBehaviour
{
    //get all the enemies int the range
    //get the closest enemy
    //rotate the barrel of turret towards enemy
    //apply damage to enemy
    //some particles muzzleflash and explosion effect
    public bool canAttack;
    public string enemyTag;
    public float attackRange;

    [Space(15)]
    public Transform target;
    public Transform turretBarrel;
    public float BarrelrotateSpeed;

    private void Awake()
    {
        canAttack = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // getting all the enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //some float for shortest distance
        float shortestDistance = Mathf.Infinity;
        //closest enemy
        GameObject closestEnemy = null;//start with null

        //loop through every enemy in enemyTag
        foreach (GameObject en in enemies)
        {
            //get the distance from turret to each enemy
            float EnemyDistance = Vector3.Distance(transform.position, en.transform.position);
            if (EnemyDistance < shortestDistance)
            {
                //getting distance of closest enemy
                shortestDistance = EnemyDistance;
                closestEnemy = en;

            }

            //closest enemy is found
            //check if we already have found our enemy

            if (closestEnemy != null && shortestDistance <= attackRange && canAttack)
            {
                //some gameObject target = enemy
                target = closestEnemy.transform;
                //getthe health script from Enemy
                //rotate barrel towards target
                LookAtTarget();
                //attacking
            }


        }
    }

    void LookAtTarget()
    {
        //rotate towards target
        Vector3 tarDir = target.position - transform.position;

        Quaternion lookTarget = Quaternion.LookRotation(tarDir);
        Vector3 rotate = Quaternion.Lerp(turretBarrel.rotation, lookTarget, Time.deltaTime * BarrelrotateSpeed).eulerAngles;

        //apply rotation to barrel
        turretBarrel.rotation = Quaternion.Euler(rotate.x, rotate.y, 0f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
