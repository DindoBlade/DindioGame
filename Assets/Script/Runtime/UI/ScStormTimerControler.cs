using System;
using TMPro;
using UnityEngine;

public class ScStormTimerControler : MonoBehaviour
{
    public bool isCountdown;
    
    [SerializeField] private float timeCounter;
    [SerializeField] private float countdownTimer; 
    [SerializeField] private TextMeshProUGUI timerText;
    
    private int minutes;
    private int seconds;

    private void Update()
    {
        if (isCountdown && countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            minutes = Mathf.FloorToInt(countdownTimer / 60);
            seconds = Mathf.FloorToInt(countdownTimer - minutes * 60);
        } else if (!isCountdown)
        {
            timeCounter += Time.deltaTime;
            minutes = Mathf.FloorToInt(timeCounter / 60);
            seconds = Mathf.FloorToInt(timeCounter - minutes * 60);
        }
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
