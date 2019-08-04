using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMove : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float newDirectionDelay = 3f;

    private Vector2 direction = Vector2.zero;
    private Rigidbody2D rb2d;
    private float nextActionTime;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        nextActionTime = Time.time;
    }

    void Update() {
        if (Time.time > nextActionTime) {
            nextActionTime += newDirectionDelay + Random.value;
            direction = new Vector2(Random.value - 0.5f, Random.value - 0.5f).normalized * speed;
        }
    }

    void FixedUpdate() {
        rb2d.velocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        direction = direction * -1;
    }
}
