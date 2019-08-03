using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private float maxBattery = 100f;
    public float currentBattery = 100f;

    [SerializeField] private float batteryDischargeRate = 2f;
    [SerializeField] private float batteryChargeRate = 10f;
    [SerializeField] private float pushBackForce = 10f;
    [SerializeField] private float invulnPeriod = 1.5f;

    private bool isCharging = false;
    private bool isDischarging = false;
    private bool isInvulnerable = false;
    private float isEmptyBuffer = 0.1f;

    // Update is called once per frame
    void Update()
    {
        float dischargeAmount = isDischarging ? Time.deltaTime * batteryDischargeRate : 0;
        float chargeAmount = isCharging ? Time.deltaTime * batteryChargeRate : 0;
        float newCharge = currentBattery + chargeAmount - dischargeAmount;
        currentBattery = Mathf.Clamp(newCharge, 0, maxBattery);

        Debug.Log("Battery: " + currentBattery);
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

    public void TakeDamage(GameObject enemy, int damage) {
        //if (collision.gameObject.tag == "Enemy") {
        //    Vector2 closestPosition = collision.collider.ClosestPoint(transform.position);
        //    Vector2 pushVector = new Vector2(transform.position.x, transform.position.y) - closestPosition;
        //    collision.rigidbody.AddForce(pushVector.normalized * pushBackForce);

        //    if (!isInvulnerable) {
        //        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        //        enemy.GiveDamage(this);
        //        StartCoroutine(BeginInvulnerability());
        //    }
        //}   
    }

    public void TakeDamage(int damage) {
        currentBattery -= damage;
        if (currentBattery <= 0) {
            Die();
        }
    }

    private void Die() {
        // TODO: Death animation and death state.
    }

    private IEnumerator BeginInvulnerability() {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnPeriod);
        isInvulnerable = false;
    }
}
