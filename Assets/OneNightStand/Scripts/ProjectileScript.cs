using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float damageAmount = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);

        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<BatteryController>().TakeDamage(damageAmount);
        }
    }
}
