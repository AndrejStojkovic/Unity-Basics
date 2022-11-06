using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    float time = 0.0f;
    bool started = false;

    public TextMeshProUGUI timerText;

    public void StartTimer() {
        started = true;
    }

    public void StopTimer() {
        started = false;
    }

    void Update() {
        if (started) {
            time += Time.deltaTime;
            timerText.text = time.ToString("0.000") + " s";
        }
    }
}
