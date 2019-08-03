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

    [SerializeField] private int rayConeSteps = 10;
    [SerializeField] private float rayDist = 20;
    [SerializeField] private float dmgRatePerRay = 1.0f;

    public LayerMask includeInRaycastMask;

    void Start()
    {
        batteryController = gameObject.GetComponent<BatteryController>();
        batteryController.setDischarging(false);

        spotAngle = (maxSpotAngle + minSpotAngle) / 2.0f;
        attachedLight.range = rayDist;
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
        Shine(focusing, lit);
    }

    private void Shine(int focus, bool isLit) {
        if (isLit != this.isLit) {
            this.isLit = isLit;
            batteryController.setDischarging(isLit);
            attachedLight.enabled = isLit;
        }

        castLightRays();
        if(isLit)
        {
            // cast rays to damage enemies
            castLightRays();
        }
    }

    private void castLightRays()
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
            potentiallyDoDamage(hitPos);


            RaycastHit2D hitNeg = Physics2D.Raycast(origin, rayDirPos, rayDist, includeInRaycastMask);
            Debug.DrawRay(origin, rayDist*rayDirNeg, Color.green);
            potentiallyDoDamage(hitNeg);
        }
    }

    private void potentiallyDoDamage(RaycastHit2D hit)
    {
        if(hit.collider != null)
        {
            if(hit.collider.tag == "Enemy")
            {
                // do damage here
                var enemyController = hit.collider.transform.gameObject.GetComponent<EnemyController>();
                enemyController.Damage(dmgRatePerRay*Time.deltaTime);
            }
        }
        
    }
}
