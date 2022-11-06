using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public Rigidbody2D rb { get; private set; }
    public float speed = 10f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        ResetBall();
    }

    private void RandomTrajectory() {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        rb.AddForce(force.normalized * speed);
    }

    public void ResetBall() {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        Invoke(nameof(RandomTrajectory), 1f);
    }

    private void FixedUpdate() {
        rb.velocity = rb.velocity.normalized * speed;
    }
}
