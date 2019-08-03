using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private GameObject lamp; 

        
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        //private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        //private bool m_FacingDown = true;
        //private bool m_isDiagonal = false;

        private void Awake()
        {
            // Setting up references.
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {

        }


        public void Move(float hMove, float vMove, int focus)
        {

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            m_Anim.SetFloat("hSpeed", Mathf.Abs(hMove));
            m_Anim.SetFloat("vSpeed", Mathf.Abs(vMove));

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(hMove * m_MaxSpeed, vMove * m_MaxSpeed);

            // TODO: Add focus handling
        }
    }
}
