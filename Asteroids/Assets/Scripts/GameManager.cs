using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public Player player;
    public ParticleSystem explosion;

    public TextMeshProUGUI livesUI;
    public TextMeshProUGUI scoreUI;

    public float respawnRate = 3.0f;
    public float safeRate = 3.0f;

    public int lives = 3;
    public int score = 0;

    public Color defaultColor = Color.white;
    public Color safeColor = Color.yellow;

    private void Awake() {
        UpdateStats();
    }

    public void AsteroidDestroyed(Asteroid asteroid) {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        if (asteroid.size < 0.75f) score += 100;
        else if (asteroid.size < 1.2f) score += 50;
        else score += 25;

        UpdateStats();
    }

    public void PlayerDied() {
        lives--;

        UpdateStats();

        explosion.transform.position = player.transform.position;
        explosion.Play();

        if(lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), respawnRate);
        }
    }

    private void Respawn() {
        player.transform.position = Vector3.zero;
        player.transform.eulerAngles = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("Safe");
        player.gameObject.SetActive(true);
        player.SetColor(safeColor);

        Invoke(nameof(TurnOnCollision), safeRate);
    }

    public void TurnOnCollision() {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
        player.SetColor(defaultColor);
    }

    private void UpdateStats() {
        scoreUI.text = score.ToString();
        livesUI.text = lives.ToString();

        float width = score < 10000 ? 90.0f : score < 100000 ? 110.0f : 130.0f;

        scoreUI.rectTransform.sizeDelta = new Vector2(width, scoreUI.rectTransform.sizeDelta.y);
    }

    private void GameOver() {
        lives = 3;
        score = 0;

        Invoke(nameof(Respawn), respawnRate);
    }
}
