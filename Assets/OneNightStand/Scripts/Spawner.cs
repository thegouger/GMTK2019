using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float waitInSeconds;

    [SerializeField] private GameObject rat;
    [SerializeField] private GameObject hammond;
    [SerializeField] private float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelaySpawn(delay));
        SpawnCheck();
    }

    private IEnumerator DelaySpawn(float wait) {
        yield return new WaitForSeconds(Random.value * wait + delay);
    }

    private void SpawnCheck() {
        if (GlobalState.currentSpawn < GlobalState.spawnMax) {
            Spawn();
            DelaySpawn(waitInSeconds);
            SpawnCheck();
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
