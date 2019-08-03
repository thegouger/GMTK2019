using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class LampController : MonoBehaviour
{
    private float m_InputBufferLimit = 0.001f;
    private BatteryController batteryController;

    private bool isLit = false;

    public Light attachedLight;
    private float spotAngle = 0.0f;
    [SerializeField] private float maxSpotAngle = 60.0f;
    [SerializeField] private float minSpotAngle = 20.0f;
    [SerializeField] private float spotAngleChangeRate = 15.0f; // deg / s

    void Start()
    {
        batteryController = gameObject.GetComponent<BatteryController>();
        batteryController.setDischarging(false);

        spotAngle = (maxSpotAngle + minSpotAngle) / 2.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float focus = CrossPlatformInputManager.GetAxisRaw("Fire1");
        float expand = CrossPlatformInputManager.GetAxisRaw("Fire2");
        float litAsFloat = CrossPlatformInputManager.GetAxisRaw("Jump");
        bool lit = Math.Abs(litAsFloat) >= m_InputBufferLimit;

        int focusing = 0;
        if (Math.Abs(focus) > m_InputBufferLimit) {
            focusing = 1;
            spotAngle += spotAngleChangeRate * Time.deltaTime;
            spotAngle = Mathf.Clamp(spotAngle, minSpotAngle, maxSpotAngle);
            attachedLight.spotAngle = spotAngle;
        } else if (Math.Abs(expand) > m_InputBufferLimit) {
            focusing = -1;
            spotAngle -= spotAngleChangeRate * Time.deltaTime;
            spotAngle = Mathf.Clamp(spotAngle, minSpotAngle, maxSpotAngle);
            attachedLight.spotAngle = spotAngle;
        }
        Shine(focusing, lit);
    }

    private void Shine(int focus, bool isLit) {
        if (isLit != this.isLit) {
            this.isLit = isLit;
            batteryController.setDischarging(isLit);
            attachedLight.enabled = isLit;
        }
    }
}
