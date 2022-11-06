using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    private Vector2 direction = Vector2.right;
    private List<Transform> segments = new List<Transform>();
    public Transform snakePrefab;
    public Vector3 startPosition;
    public int initialSize = 3;

    private void Start() {
        ResetState();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) direction = Vector2.right;
    }

    private void FixedUpdate() {
        for(int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = new Vector3(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y),
            0.0f
        );
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Food") {
            Grow();
        } else if(other.gameObject.tag == "Obstacle") {
            ResetState();
        }
    }

    private void Grow() {
        Transform segment = Instantiate(snakePrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    private void ResetState() {
        for(int i = 1; i < segments.Count; i++)
            Destroy(segments[i].gameObject);

        segments.Clear();
        segments.Add(transform);

        transform.position = startPosition;

        for (int i = 1; i < initialSize; i++)
            segments.Add(Instantiate(snakePrefab));

        direction = Vector2.right;
    }
}
