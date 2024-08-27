using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour, IScoreChange
{
    public event EventHandler<IScoreChange.OnPlayerScoreChangedEventArgs> OnPlayerScoreChanged;

    private float playerScore = 0f;

    private void Awake()
    {
        UpdatePlayerScore(playerScore);
    }

    public void UpdatePlayerScore(float score)
    {
        playerScore += score;

        if (playerScore < 0)
        {
            //playerScore = 0;
        }

        OnPlayerScoreChanged?.Invoke(this, new IScoreChange.OnPlayerScoreChangedEventArgs { currentPlayerScore = playerScore });
        //Debug.Log(playerScore);
    }
}
