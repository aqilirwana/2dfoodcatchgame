using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreChange
{
    // Create an event of player score changed
    public event EventHandler<OnPlayerScoreChangedEventArgs> OnPlayerScoreChanged;
    public class OnPlayerScoreChangedEventArgs : EventArgs
    {
        public float currentPlayerScore;
    }
}
