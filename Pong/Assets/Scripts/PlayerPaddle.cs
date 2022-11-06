using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : Paddle {
    private Vector2 dir;

    private void Update() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) dir = Vector2.up;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) dir = Vector2.down;
        else dir = Vector2.zero;
    }

    private void FixedUpdate() {
        if(dir.sqrMagnitude != 0) {
            rb.AddForce(dir * speed);
        }
    }
}
