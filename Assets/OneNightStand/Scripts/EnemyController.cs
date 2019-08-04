using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float health = 1000;
    [SerializeField] internal float damageRate = 10;

    [SerializeField] internal float damageRadius = 5;

    public GameObject player;

    private bool isDead = false;

    void Start()
    {
    }
    
    void Update()
    {
        var center = transform.Find("Center");
        if((player.transform.position - center.transform.position).magnitude < damageRadius)
        {
            //Debug.Log("Damaging player");
            var batteryController = player.GetComponent<BatteryController>();
            GiveDamage(batteryController);
        }
    }

    public void Damage(float dmg) {
        health -= dmg;
        if (health <= 0 && !isDead) {
            Die();
        }
    }

    public void GiveDamage(BatteryController controller) {
        if(!isDead)
        {
            // Give damage to player's BatteryController 
            controller.TakeDamage(damageRate*Time.deltaTime);
        }
    }

    // Each individual needs to implement their own death due to animation.
    private void Die()
    {
        isDead = true;


        var aiPath = GetComponent<Pathfinding.AIPath>();
        if(aiPath)
            aiPath.canMove = false;
        
        var turtleMove = GetComponent<TurtleMove>();
        if(turtleMove)
        {
            turtleMove.speed = 0.0f;
        }

        var rgdb = GetComponent<Rigidbody2D>();
        if(rgdb)
        {
            Destroy(rgdb);
        }

        var center = transform.Find("Center");
        center.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        GetComponent<Animator>().SetBool("isDead", true);
    }
}
