using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract public class EnemyController : MonoBehaviour
{
    [SerializeField] internal int health = 1000;

    void Start()
    {
    }
    
    void Update()
    {
    }

    public void Damage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            Die();
        }
    }

    // Each individual needs to implement their own death due to animation.
    internal abstract void Die();
}
