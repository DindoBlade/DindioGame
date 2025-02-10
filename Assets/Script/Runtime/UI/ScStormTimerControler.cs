using System;
using TMPro;
using UnityEngine;

public class ScStormTimerControler : MonoBehaviour
{
    public bool _isCountdown;
    [SerializeField] private float _timeCounter;
    [SerializeField] private float _countdownTimer; 
    [SerializeField] private TextMeshProUGUI _timerText;
    
    private int _minutes;
    private int _seconds;
    private float _initialTimer;

    public static ScStormTimerControler Instance;

    private void Awake() {
        _initialTimer = _countdownTimer;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (_isCountdown && _countdownTimer > 0)
        {
            _countdownTimer -= Time.deltaTime;
            _minutes = Mathf.FloorToInt(_countdownTimer / 60);
            _seconds = Mathf.FloorToInt(_countdownTimer - _minutes * 60);

        } else if (!_isCountdown)
        {
            _timeCounter += Time.deltaTime;
            _minutes = Mathf.FloorToInt(_timeCounter / 60);
            _seconds = Mathf.FloorToInt(_timeCounter - _minutes * 60);
        }

        if (_isCountdown && _countdownTimer <= 0 )
        ResetTimer();
        // reduce zone if possible

        _timerText.text = $"{_minutes:00}:{_seconds:00}";
    }   

    public void ResetTimer()
    {
        _countdownTimer = _initialTimer;
    } 
}
