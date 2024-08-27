using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //public event EventHandler OnStartClicked;
    public event EventHandler OnResumeClicked;
    public event EventHandler OnPauseClicked;
    public event EventHandler OnRestartClicked;
    public event EventHandler OnHomeClicked;

    [SerializeField] private Player player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform scoreUI;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Transform startScreen;
    [SerializeField] private Transform gameScreen;
    [SerializeField] private Transform pauseScreen;
    [SerializeField] private Transform gameEndScreen;
    [SerializeField] private Transform gameOverScreen;

    [SerializeField] private Transform wheelImage;
    [SerializeField] private Transform scoreEnd;
    [SerializeField] private Transform gameOverText;

    private void Awake()
    {
        gameManager.OnGameEnded += GameManager_OnGameEnded;
        gameManager.OnGameOver += GameManager_OnGameOver;

        player.OnFirstStart += Player_OnFirstStart;

        scoreEnd.localScale = new Vector3(0f, 0f, 0f);
        LeanTween.reset();
    }

    private void Player_OnFirstStart(object sender, EventArgs e)
    {
        Debug.Log("OnFirstStart!!!");

        startScreen.gameObject.SetActive(true);
        gameScreen.gameObject.SetActive(false);
    }

    // Home button click event
    public void ClickHome()
    {
        startScreen.gameObject.SetActive(true);
        gameScreen.gameObject.SetActive(false);

        gameEndScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);

        OnHomeClicked?.Invoke(this, EventArgs.Empty);
    }

    // Start(onFirstStart) button click event
    public void ClickStart()
    {
        startScreen.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);

        gameEndScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);

        //OnStartClicked?.Invoke(this, EventArgs.Empty);
        OnRestartClicked?.Invoke(this, EventArgs.Empty);
    }

    // Play button click event
    public void ClickResume()
    {
        OnResumeClicked?.Invoke(this, EventArgs.Empty);

        gameScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
    }

    // Pause button click event
    public void ClickPause()
    {
        OnPauseClicked?.Invoke(this, EventArgs.Empty);


        gameScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(true);
    }

    public void ClickRestart()
    {
        OnRestartClicked?.Invoke(this, EventArgs.Empty);
        Player.Instance.playerVisual.gameObject.SetActive(true);
    }

    private void GameManager_OnGameEnded(object sender, GameManager.OnGameEndedEventArgs e)
    {
        gameScreen.gameObject.SetActive(false);

        gameEndScreen.gameObject.SetActive(true);

        LeanTween.rotateAround(wheelImage.gameObject, Vector3.forward, -360, 10f).setLoopClamp();
        LeanTween.scale(scoreEnd.gameObject, new Vector3(1f, 1f, 1f), 2f).setDelay(1f).setEase(LeanTweenType.easeOutElastic);
    }

    private void GameManager_OnGameOver(object sender, EventArgs e)
    {
        gameScreen.gameObject.SetActive(false);

        gameOverScreen.gameObject.SetActive(true);

        LeanTween.rotate(gameOverText.gameObject, new Vector3(0f, 0f, 15f), 0.5f).setLoopType(LeanTweenType.pingPong);
    }
}
