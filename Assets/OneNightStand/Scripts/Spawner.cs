using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float waitInSeconds;

    [SerializeField] private GameObject rat;
    [SerializeField] private GameObject hammond;
    [SerializeField] private float delay;

    private float timeAccum = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update() {
        if (GlobalState.currentSpawn < GlobalState.spawnMax) {
            timeAccum += Time.deltaTime;
            if(timeAccum > (delay + Random.Range(0, delay)))
            {
                Debug.Log("Spawning");
                timeAccum = 0.0f;
                Spawn();
            }
        }
    }

    private void Spawn() {
        GameObject critter = Random.value > 0.5 ? Instantiate(rat, transform.position, Quaternion.identity) : Instantiate(hammond, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        critter.GetComponent<Pathfinding.AIDestinationSetter>().target = player.transform;
        critter.GetComponent<EnemyController>().player = player;
        GlobalState.currentSpawn++;
    }
}
