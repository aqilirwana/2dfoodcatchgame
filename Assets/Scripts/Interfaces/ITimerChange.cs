using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimerChange
{
    // Create an event of player score changed
    public event EventHandler<OnCountdownTimerChangedEventArgs> OnCountdownTimerChanged;
    public class OnCountdownTimerChangedEventArgs : EventArgs
    {
        public int currentTime;
    }
}
