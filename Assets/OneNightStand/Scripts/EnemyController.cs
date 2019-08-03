using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] internal float health = 1000;
    [SerializeField] internal float damage = 10;

    public GameObject player;

    private bool isDead = false;

    void Start()
    {
    }
    
    void Update()
    {
        var center = transform.Find("Center");
        if((player.transform.position - center.transform.position).magnitude < 5)
        {
            //Debug.Log("Damaging player");
            var batteryController = player.GetComponent<BatteryController>();
            GiveDamage(batteryController);
        }
    }

    public void Damage(float dmg) {
        health -= dmg;
        if (health <= 0) {
            Die();
        }
    }

    public void GiveDamage(BatteryController controller) {
        if(!isDead)
        {
            controller.TakeDamage(damage);
        }
    }

    // Each individual needs to implement their own death due to animation.
    private void Die()
    {
        isDead = true;
        GetComponent<Pathfinding.AIPath>().canMove = false;
        GetComponent<Animator>().SetBool("isDead", true);
    }
}
