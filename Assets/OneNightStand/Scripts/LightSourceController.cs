using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            BatteryController batteryController = col.gameObject.GetComponent<BatteryController>();
            batteryController.setCharging(true);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player"){
            BatteryController batteryController = col.gameObject.GetComponent<BatteryController>();
            batteryController.setCharging(false);
        }
    }
}
