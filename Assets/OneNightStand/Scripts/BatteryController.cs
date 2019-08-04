using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private float maxBattery = 100f;
    public float currentBattery;

    [SerializeField] private float batteryDischargeRate = 4f;
    [SerializeField] private float batteryChargeRate = 15f;
    [SerializeField] private GameObject deathOverlay; 

    private bool isCharging = false;
    private bool isDischarging = false;
    private float isEmptyBuffer = 0.1f;
    private bool hasDied;

    private void Start() {
        Time.timeScale = 1;
        currentBattery = GlobalState.currentBattery;
        Debug.Log("Battery: " + currentBattery);
        hasDied = false;
    }

    // Update is called once per frame
    void Update() {
        float dischargeAmount = isDischarging ? Time.deltaTime * batteryDischargeRate : 0;
        float chargeAmount = isCharging ? Time.deltaTime * batteryChargeRate : 0;
        float newCharge = currentBattery + chargeAmount - dischargeAmount;
        currentBattery = Mathf.Clamp(newCharge, 0, maxBattery);
        GlobalState.currentBattery = currentBattery;
    }

    public bool toggle() {
        isDischarging = !isDischarging;
        return isDischarging;
    }

    public void SetDischarging(bool isDischarging) {
        this.isDischarging = isDischarging;
    }

    public void SetCharging(bool isCharging){
        this.isCharging = isCharging;
    }

    public bool IsEmpty() {
        return System.Math.Abs(currentBattery) < isEmptyBuffer;
    }

    public void TakeDamage(float damage) {
        currentBattery -= damage;
        Debug.Log("Player taking dmg");
        if (currentBattery <= 0 && !hasDied) {
            hasDied = true;
            Die();
        }
    }

    private void Die() {
        gameObject.GetComponent<PlatformerCharacter2D>().animateDeath();

        currentBattery = 100f;
        // Pause game
        Time.timeScale = 0;

        // show canvas
        Text deathText = deathOverlay.GetComponentInChildren<Text>();
        deathText.text = GlobalState.killCount.ToString();
        deathOverlay.SetActive(true);
    }
}
