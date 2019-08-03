using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class LampController : MonoBehaviour
{
    private float m_InputBufferLimit = 0.001f;
    private BatteryController batteryController;

    private bool isLit = false;

    void Start()
    {
        batteryController = gameObject.GetComponent<BatteryController>();
        batteryController.setDischarging(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float focus = CrossPlatformInputManager.GetAxisRaw("Fire1");
        float expand = CrossPlatformInputManager.GetAxisRaw("Fire2");
        float litAsFloat = CrossPlatformInputManager.GetAxisRaw("Jump");
        bool lit = Math.Abs(litAsFloat) >= m_InputBufferLimit;

        int focusing = 0;
        if (Math.Abs(focus) < m_InputBufferLimit) {
            focusing = 1;
        } else if (Math.Abs(expand) < m_InputBufferLimit) {
            focusing = -1;
        }
        Shine(focusing, lit);
    }

    private void Shine(int focus, bool isLit) {
        if (isLit != this.isLit) {
            this.isLit = isLit;
            batteryController.setDischarging(isLit);
        }
    }
}
