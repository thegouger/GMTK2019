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
    [SerializeField] private float maxSpotAngle = 125.0f;
    [SerializeField] private float minSpotAngle = 20.0f;
    [SerializeField] private float spotAngleChangeRate = 15.0f; // deg / s

    [SerializeField] private int rayConeSteps = 10;
    [SerializeField] private float rayDist = 20;
    [SerializeField] private float dmgRatePerRay = 1.0f;

    public LayerMask includeInRaycastMask;
    private bool litWasPressed = false;

    void Start()
    {
        batteryController = gameObject.GetComponent<BatteryController>();
        batteryController.SetDischarging(false);

        spotAngle = (maxSpotAngle + minSpotAngle) / 2.0f;
        attachedLight.spotAngle = spotAngle;
        //attachedLight.range = rayDist;
    }

    // Update is called once per frame
    void Update()
    {
        float focus = CrossPlatformInputManager.GetAxisRaw("Fire1");
        float expand = CrossPlatformInputManager.GetAxisRaw("Fire2");
        bool lit = CrossPlatformInputManager.GetButtonDown("Jump");

        if (lit && lit != litWasPressed && gameObject.GetComponent<BatteryController>().currentBattery > 5.0f) {
            isLit = batteryController.toggle();
        }
        litWasPressed = lit;

        int focusing = 0;
        if (Math.Abs(focus) > m_InputBufferLimit) {
            focusing = 1;

            // Decrease spot angle
            spotAngle += spotAngleChangeRate * Time.deltaTime;
            spotAngle = Mathf.Clamp(spotAngle, minSpotAngle, maxSpotAngle);
            attachedLight.spotAngle = spotAngle;
        } else if (Math.Abs(expand) > m_InputBufferLimit) {
            focusing = -1;

            // Increase spot angle
            spotAngle -= spotAngleChangeRate * Time.deltaTime;
            spotAngle = Mathf.Clamp(spotAngle, minSpotAngle, maxSpotAngle);
            attachedLight.spotAngle = spotAngle;
        }
        Shine(focusing);
    }

    private void Shine(int focus) {
        bool shouldCast = isLit && gameObject.GetComponent<BatteryController>().currentBattery > 5.0f;
        attachedLight.enabled = shouldCast;
        if(shouldCast) {
            CastLightRays();
        }
    }

    private void CastLightRays()
    {
        var forward = attachedLight.transform.forward;
        var origin = attachedLight.transform.position;
        for(int rayIdx = 0; rayIdx < rayConeSteps; rayIdx++)
        {
            
            var angle = spotAngle / (rayConeSteps * 2) * rayIdx;
            var rayDirPos = Quaternion.AngleAxis(angle, new Vector3(0f, 0f, 1f)) * forward;
            var rayDirNeg = Quaternion.AngleAxis(-angle, new Vector3(0f, 0f, 1f)) * forward;

            RaycastHit2D hitPos = Physics2D.Raycast(origin, rayDirPos, rayDist, includeInRaycastMask);
            Debug.DrawRay(origin, rayDist*rayDirPos, Color.green);
            PotentiallyDoDamage(hitPos);


            RaycastHit2D hitNeg = Physics2D.Raycast(origin, rayDirPos, rayDist, includeInRaycastMask);
            Debug.DrawRay(origin, rayDist*rayDirNeg, Color.green);
            PotentiallyDoDamage(hitNeg);
        }
    }

    private void PotentiallyDoDamage(RaycastHit2D hit)
    {
        if(hit.collider != null)
        {
            if(hit.collider.tag == "Enemy")
            {
                // do damage to enemy here
                var enemyController = hit.collider.transform.parent.gameObject.GetComponent<EnemyController>();
                enemyController.Damage(dmgRatePerRay * Time.deltaTime);
            } else if (hit.collider.tag == "Generator") {
                var generatorController = hit.collider.gameObject.GetComponent<GeneratorController>();
                generatorController.Charge(dmgRatePerRay * Time.deltaTime);
            }
        }
        
    }
}
