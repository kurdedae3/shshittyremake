using System.Collections;
using UnityEngine;
using UnityEngine.UI; // For standard UI Text
using TMPro; // Uncomment if using TextMeshPro

public class TimerCountUp : MonoBehaviour
{
    
    public TMP_Text timerText;

    public float elapsedTime = 0f;
    public bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartTimer() => isRunning = true;
    public void StopTimer() => isRunning = false;
    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = false;
        timerText.text = "0:00";
    }
}