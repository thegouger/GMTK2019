using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattery : MonoBehaviour
{

    private float maxHeight;
    private RectTransform rectF;
    private Text generatorText;

    private int prevGenerators = 0; 

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject charge = GameObject.Find("Charge");
        GameObject chargeF = GameObject.Find("ChargeForeground");
        generatorText = GameObject.Find("GeneratorCount").GetComponent<Text>();

        rectF = chargeF.GetComponent<RectTransform>();
        RectTransform rect = charge.GetComponent<RectTransform>();
        maxHeight = rect.sizeDelta.y;
    }

    void OnGUI()
    {
        float batteryPercent = GlobalState.currentBattery;
        float distance = batteryPercent - 50f;
        if (batteryPercent < 0) {
            batteryPercent += (-2 * distance);
        } else {
            batteryPercent -= (2 * distance);
        }
        rectF.sizeDelta = new Vector2(rectF.sizeDelta.x, maxHeight * batteryPercent / 100f);

        int completedGenerators = calculateCompletedGenerators();
        if (completedGenerators != prevGenerators) {
            generatorText.text = completedGenerators + "/5";
            prevGenerators = completedGenerators;
        }
    }

    private int calculateCompletedGenerators() {
        int count = 0;
        for (int i = 0; i < GlobalState.generators.Length ;i++) {
            if (GlobalState.generators[i]) {
                count++;
            }
        }
        return count;
    }
}
