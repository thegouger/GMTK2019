using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private float m_InputBufferLimit = 0.001f;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            int h = getDiscreteValue(CrossPlatformInputManager.GetAxisRaw("Horizontal"));
            int v = getDiscreteValue(CrossPlatformInputManager.GetAxisRaw("Vertical"));
            float focus = CrossPlatformInputManager.GetAxisRaw("Fire1");
            float expand = CrossPlatformInputManager.GetAxisRaw("Fire2");

            int focusing = 0;
            if (Math.Abs(focus) < m_InputBufferLimit) {
                focusing = 1;
            } else if (Math.Abs(expand) < m_InputBufferLimit) {
                focusing = -1;
            }

            m_Character.Move(h, v, focusing);
            m_Jump = false;
        }

        private int getDiscreteValue(float val) {
            if (val > m_InputBufferLimit) {
                return 1;
            } else if (val < m_InputBufferLimit * -1) {
                return -1;
            } else {
                return 0;
            }
        }
    }
}
