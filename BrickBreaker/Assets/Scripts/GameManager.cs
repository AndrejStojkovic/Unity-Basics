using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }

    public TextMeshProUGUI livesUI;
    public TextMeshProUGUI scoreUI;

    const int NUM_LEVELS = 2;

    public int level = 1;
    public int score = 0;
    public int lives = 3;

    private void Awake() {
        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start() {
        NewGame();
    }

    private void NewGame() {
        score = 0;
        lives = 3;
        level = 1;
        LoadLevel(level);
    }

    private void LoadLevel(int l) {
        level = l;

        if(level > NUM_LEVELS) level = 1;

        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode) {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
        livesUI = GameObject.FindGameObjectWithTag("Lives").GetComponent<TextMeshProUGUI>();
        scoreUI = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();

        UpdateUI();
    }

    public void Miss() {
        lives--;

        UpdateUI();

        if (lives > 0) ResetLevel();
        else GameOver();
    }

    public void ResetLevel() {
        ball.ResetBall();
        paddle.ResetPaddle();
    }

    private void GameOver() {
        NewGame();
    }

    public void Hit(Brick brick) {
        score += brick.points;

        UpdateUI();

        if (Cleared()) {
            LoadLevel(level + 1);
        }
    }

    private bool Cleared() {
        for(int i = 0; i < bricks.Length; i++) {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable) {
                return false;
            }
        }

        return true;
    }

    private void UpdateUI() {
        livesUI.text = lives.ToString();
        scoreUI.text = score.ToString();
    }
}
