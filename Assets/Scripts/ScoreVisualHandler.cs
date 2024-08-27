using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreVisualHandler : MonoBehaviour
{
    [SerializeField] private Score score;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI endScoreText;

    private IScoreChange scoreChange;

    private void Awake()
    {
        scoreChange = score.GetComponent<IScoreChange>();

        if (scoreChange == null)
        {
            Debug.LogError("Game Object " + score + " does not have a component that implement IScoreChange!");
        }

        scoreChange.OnPlayerScoreChanged += ScoreChange_OnPlayerScoreChanged;
    }

    private void ScoreChange_OnPlayerScoreChanged(object sender, IScoreChange.OnPlayerScoreChangedEventArgs e)
    {
        UpdateScoreText(e.currentPlayerScore);
    }

    private void UpdateScoreText(float newScore)
    {
        playerScoreText.text = newScore.ToString();
        endScoreText.text = newScore.ToString();
    }
}
