using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private bool thrusting;
    private float dir;
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public Bullet bulletPrefab;
    public float thrustSpeed = 1.0f;
    public float rotateSpeed = 1.0f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        thrusting = Input.GetKey(KeyCode.W);

        if(Input.GetKey(KeyCode.A)) dir = 1.0f;
        else if(Input.GetKey(KeyCode.D)) dir = -1.0f;
        else dir = 0.0f;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) Shoot();
    }

    private void FixedUpdate() {
        if(thrusting) rb.AddForce(transform.up * 1.0f);

        if (dir != 0.0f) rb.AddTorque(dir * rotateSpeed);
    }

    private void Shoot() {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Asteroid") {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0.0f;

            gameObject.SetActive(false);
            gameManager.PlayerDied();
        }
    }

    public void SetColor(Color color) {
        spriteRenderer.color = color;
    }
}
