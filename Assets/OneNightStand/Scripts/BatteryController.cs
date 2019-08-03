using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private float maxBattery = 100f;
    [SerializeField] private float currentBattery = 100f;

    [SerializeField] private float batteryDischargeRate = 2f;
    [SerializeField] private float batteryChargeRate = 10f;

    private bool isCharging = false;
    private bool isDischarging = false;
    private float isEmptyBuffer = 0.1f;

    // Update is called once per frame
    void Update()
    {
        float dischargeAmount = isDischarging ? Time.deltaTime * batteryDischargeRate : 0;
        float chargeAmount = isCharging ? Time.deltaTime * batteryChargeRate : 0;
        float newCharge = currentBattery + chargeAmount - dischargeAmount;
        currentBattery = Mathf.Clamp(newCharge, 0, maxBattery);

        Debug.Log(currentBattery);
    }

    public void setDischarging(bool isDischarging) {
        this.isDischarging = isDischarging;
    }

    public void setCharging(bool isCharging){
        this.isCharging = isCharging;
    }

    public bool isEmpty() {
        return System.Math.Abs(currentBattery) < isEmptyBuffer;
    }
}
