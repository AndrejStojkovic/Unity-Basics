using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    protected Rigidbody2D rb;
    public float speed = 10.0f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPaddle() {
        rb.position = new Vector2(rb.position.x, 0.0f);
    }
}
