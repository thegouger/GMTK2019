﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNai : MonoBehaviour
{

    public GameObject nailProjectile;
    public float fireRate = 0.5f;

    public float fireSpeed = 20.0f;

    private float timeAccum = 0.0f;

    public AudioClip shootAudio;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(timeAccum < 1.0f/fireRate + Random.Range(0, 0.5f/fireRate))
        {
            timeAccum += Time.deltaTime;
        }
        else
        {
            timeAccum = 0.0f;
            Shoot();
        }
    }

    void Shoot() {
        var health = GetComponent<EnemyController>().health;

        if(health > 0.0f)
        {
            var dir = (player.transform.position - transform.position).normalized;
            GameObject clone = (GameObject)Instantiate(nailProjectile, transform.position, Quaternion.identity);
            clone.transform.right = -dir;
            clone.GetComponent<Rigidbody2D>().velocity = (dir * fireSpeed);

            GetComponent<AudioSource>().PlayOneShot(shootAudio);
        }
    }
}
