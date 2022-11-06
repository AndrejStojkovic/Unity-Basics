using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private GameManager gameManager;
    public Sprite[] sprites;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float maxTime = 30.0f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;

        rb.mass = size;
    }

    public void SetTrajectory(Vector2 direction) {
        rb.AddForce(direction * speed);

        Destroy(gameObject, maxTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Bullet") {
            if(size >= (2 * minSize)) {
                CreateSplit();
                CreateSplit();
            }

            gameManager.AsteroidDestroyed(this);
            Destroy(gameObject);
        }
    }

    private void CreateSplit() {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size / 2.0f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * speed);
    }
}
