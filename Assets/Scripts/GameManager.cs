using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    private int playerScore;
    private int cpuScore;
    public PlayerPaddle player;
    public CPUPaddle cpu;
    public Ball ball;

    public TextMeshProUGUI playerUI;
    public TextMeshProUGUI cpuUI;

    public void PlayerScores() {
        playerScore++;
        SetScore();
        ResetGame();
    }
    public void CPUScores() {
        cpuScore++;
        SetScore();
        ResetGame();
    }

    private void SetScore() {
        playerUI.text = playerScore.ToString();
        cpuUI.text = cpuScore.ToString();
    }

    private void ResetGame() {
        ball.ResetPosition();
        ball.StartForce();
        player.ResetPaddle();
        cpu.ResetPaddle();
        
    }
}
