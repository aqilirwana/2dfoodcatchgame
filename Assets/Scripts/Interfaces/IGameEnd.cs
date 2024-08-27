using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEnd
{
    // Create an event of game ended
    public event EventHandler<OnGameEndedEventArgs> OnGameEnded;
    public class OnGameEndedEventArgs : EventArgs
    {
        public float endScore;
        public int endTime;
    }
}
