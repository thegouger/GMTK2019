using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] internal int health = 1000;
    [SerializeField] internal int damage = 10;

    void Start()
    {
    }
    
    void Update()
    {
    }

    public void Damage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            //Die();
        }
    }

    public void GiveDamage(BatteryController controller) {
        controller.TakeDamage(damage);
    }

    public void OnControllerColliderHit(ControllerColliderHit hit) {
        Debug.Log("Controller HIT");
        if (hit.gameObject.tag == "Player") {
            BatteryController controller = hit.gameObject.GetComponent<BatteryController>();
            controller.TakeDamage(gameObject, damage);
        }
    }

    // Each individual needs to implement their own death due to animation.
    //internal abstract void Die();
}
