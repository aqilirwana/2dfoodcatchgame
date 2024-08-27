using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerVisualHandler : MonoBehaviour
{
    [SerializeField] private Timer timerSystem;
    [SerializeField] private TextMeshProUGUI timerCountdownText;

    private ITimerChange timerChange;

    private void Awake()
    {
        timerChange = timerSystem.GetComponent<ITimerChange>();

        if (timerChange == null)
        {
            Debug.LogError("Game Object " + timerSystem + " does not have a component that implement ITimerChange!");
        }
        timerSystem.OnCountdownTimerChanged += GameManager_OnCountdownTimerChanged;
    }

    private void GameManager_OnCountdownTimerChanged(object sender, ITimerChange.OnCountdownTimerChangedEventArgs e)
    {
        UpdateTimerText(e.currentTime);
    }

    private void UpdateTimerText(int newTime)
    {
        timerCountdownText.text = newTime.ToString() + "s";
    }
}
