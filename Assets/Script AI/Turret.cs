using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    /* 1. Turret melihat enemy
     * 2. Rotasi ke enemy terdekat
     * 3. Mengambil enemy dengan jarak terdekat
     * 4. Apply damage
     * 5. Partikel(?)
     */

    //Var
    public bool canAtk;
    public string enemytag;
    public float attackrange;

    [Space(15)]
    public Transform target;
    public Transform turretbarrel;
    public float rotatespeed;
    [Space(15)]
    public float firerate = 0.5f;
    float nextfiretime = 1f;

    EnemyAI targetedenemy;
    [Space(15)]
    public int atkdmg;

    private void Start()
    {
        canAtk = true;
    }

    private void Update()
    {
        //Get all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
        //get distance for shortest distance
        float shortdistance = Mathf.Infinity;
        //Closest Enemy
        GameObject closestEnemy = null; //start with none

        //Loop enemy with tag
        foreach (GameObject en in enemies)
        {
            //Get distance from turret
            float enemyDistance = Vector3.Distance(
                transform.position,
                en.transform.position
                );
            //Declare if enemy distance enters the shortest distance
            if(enemyDistance < shortdistance)
            {
                //Get the closest enemies
                shortdistance = enemyDistance;
                closestEnemy = en;
            }

            //Closest enemies found
            //Check if turret already found enemies

            if(closestEnemy != null && shortdistance <= attackrange && canAtk)
            {
                //Declare closest enemy is the target
                target = closestEnemy.transform;
                //Get the enemy health script
                targetedenemy = closestEnemy.GetComponent<EnemyAI>();
                //Rotate barrel towards approaching enemy
                LookAtTarget();

                //Attacking
                if (canAtk && (Time.time > nextfiretime))
                {
                    nextfiretime = Time.time + firerate;
                    ApplyDamage();
                }
            }

        }
    }

    public void ApplyDamage()
    {
        Debug.Log("hit");
        targetedenemy.takeDamage(atkdmg);
    }

    void LookAtTarget()
    {
        //Rotate towards enemy
        Vector3 targetdirection = target.position - transform.position;
        Quaternion lookTarget = Quaternion.LookRotation(targetdirection);
        Vector3 rotate = Quaternion.Lerp(
            turretbarrel.rotation, 
            lookTarget, 
            Time.deltaTime * rotatespeed
            ).eulerAngles;

        //Apply rotation to the barrel
        turretbarrel.rotation = Quaternion.Euler(rotate.x, rotate.y, 0f);
    }

    //For see the range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }

}
