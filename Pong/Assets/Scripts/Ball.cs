using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed = 200.0f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        ResetPosition();
        StartForce();
    }

    public void ResetPosition() {
        rb.position = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    public void StartForce() {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);

        Vector2 dir = new Vector2(x, y);
        rb.AddForce(dir * speed);
    }

    public void AddForce(Vector2 force) {
        rb.AddForce(force);
    }
}
