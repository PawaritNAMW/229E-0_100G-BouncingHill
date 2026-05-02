using UnityEngine;
using TMPro;

public class SimpleTimer : MonoBehaviour
{
    public float timeElapsed = 0f;
    public bool isRunning = true;

    public TMP_Text timerText;

    void Update()
    {
        if (!isRunning) return;

        timeElapsed += Time.deltaTime;

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);

        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }
}