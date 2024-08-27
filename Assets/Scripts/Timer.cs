using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour, ITimerChange
{
    public event EventHandler<ITimerChange.OnCountdownTimerChangedEventArgs> OnCountdownTimerChanged;

    private Coroutine countdownCoroutine;

    void Start()
    {
        
    }

    public IEnumerator Countdown(int dur)
    {
        for (int i = dur; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
            //Debug.Log(Mathf.RoundToInt(i));
            OnCountdownTimerChanged?.Invoke(this, new ITimerChange.OnCountdownTimerChangedEventArgs { currentTime = i });

            if (i <= 0)
            {
                //Debug.Log("Timer has ended! " + i + "s.");
                //Debug.Log(lastScore);
            }
        }
    }

    public void StartCountdown(int duration)
    {
        // Reset the countdown if it's already running
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        // Start the countdown with the new duration
        countdownCoroutine = StartCoroutine(Countdown(duration));
    }

    public void ResetCountdown(int duration)
    {
        StartCountdown(duration);
    }
}
