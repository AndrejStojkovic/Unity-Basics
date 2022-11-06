using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed = 500.0f;
    public float maxTime = 10.0f;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction) {
        rb.AddForce(direction * speed);

        Destroy(gameObject, maxTime); 
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
