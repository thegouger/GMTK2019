using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattery : MonoBehaviour
{

    private BatteryController batteryController;
    private float maxHeight;

    private RectTransform rectF;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        batteryController = player.GetComponent<BatteryController>();
        GameObject charge = GameObject.Find("Charge");
        GameObject chargeF = GameObject.Find("ChargeForeground");

        rectF = chargeF.GetComponent<RectTransform>();
        RectTransform rect = charge.GetComponent<RectTransform>();
        maxHeight = rect.sizeDelta.y;
    }

    void OnGUI()
    {
        float batteryPercent = batteryController.currentBattery;
        float distance = batteryPercent - 50f;
        if (batteryPercent < 0) {
            batteryPercent += (-2 * distance);
        } else {
            batteryPercent -= (2 * distance);
        }
        rectF.sizeDelta = new Vector2(rectF.sizeDelta.x, maxHeight * batteryPercent / 100f);
    }
}
