using System;
using UnityEngine;


public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private GameObject lamp;
    [SerializeField] private GameObject deskClutter;

    
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        // Setting up references.
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
    }

    private void animateDeath()
    {
        m_Anim.SetBool("isDead", true);
        deskClutter.GetComponent<Animator>().SetBool("isDead", true);
    }

    public void Move(float hMove, float vMove)
    {

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        m_Anim.SetFloat("hSpeed", Mathf.Abs(hMove));
        m_Anim.SetFloat("vSpeed", Mathf.Abs(vMove));

        // Move the character
        m_Rigidbody2D.velocity = new Vector2(hMove * m_MaxSpeed, vMove * m_MaxSpeed);
    }
}
