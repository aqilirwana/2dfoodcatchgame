using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event EventHandler<OnGameEndedEventArgs> OnGameEnded;
    public class OnGameEndedEventArgs : EventArgs
    {
        public float score;
    }

    public event EventHandler OnGameOver;

    [SerializeField] private Player player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Timer gameTimer;
    [SerializeField] private int duration;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private float spawnTimeInterval;
    [SerializeField] private Score score;

    private IEnumerator timer;
    private IEnumerator spawner;
    private IScoreChange scoreChange;

    private float lastScore;
    private int lastTimer;

    private void Start()
    {
        
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        player.OnFirstStart += Player_OnFirstStart;

        // Subs Timer
        if (gameTimer == null)
        {
            Debug.LogError("Please assign Timer in the inspector of GameManager");
        }
        else
        {
            gameTimer.OnCountdownTimerChanged += GameTimer_OnCountdownTimerChanged;
            timer = gameTimer.Countdown(duration);
        }

        // Subs Spawner
        if (itemSpawner == null)
        {
            Debug.LogError("Please assign ItemSpawner in the inspector of GameManager");
        }
        else
        {
            spawner = itemSpawner.SpawnFood(spawnTimeInterval);
        }

        // Subs Score
        scoreChange = score.GetComponent<IScoreChange>();

        if (scoreChange == null)
        {
            Debug.LogError("Game Object " + score + " does not have a component that implement IScoreChange!");
        }

        scoreChange.OnPlayerScoreChanged += ScoreChange_OnPlayerScoreChanged;

        // Subs UIManager event
        uiManager.OnResumeClicked += UiManager_OnResumeClicked;
        uiManager.OnPauseClicked += UiManager_OnPauseClicked;
        uiManager.OnRestartClicked += UiManager_OnRestartClicked;
        uiManager.OnHomeClicked += UiManager_OnHomeClicked;

        OnResumeGame(spawner, timer);
    }

    private void UiManager_OnHomeClicked(object sender, EventArgs e)
    {
        OnStartGame(spawner, timer);
    }

    // Received First Start event do below
    private void Player_OnFirstStart(object sender, EventArgs e)
    {
        OnStartGame(spawner, timer);
    }

    // START the game by
    private void OnStartGame(IEnumerator foodSpawn, IEnumerator countTimer)
    {
        StopCoroutine(foodSpawn);
        StopCoroutine(countTimer);
    }

    // Received RESUME BUTTON click event
    private void UiManager_OnResumeClicked(object sender, EventArgs e)
    {
        OnResumeGame(spawner, timer);
    }

    // RESUME the game by: RESUME the Countdown Timer/ENABLE the touch-move player/START spawning the food
    private void OnResumeGame(IEnumerator foodSpawn, IEnumerator countTimer)
    {
        //isPaused = false;
        StartCoroutine(countTimer);
        StartCoroutine(foodSpawn);
        Player.Instance.isPlaying = true;
    }

    // Received PAUSE BUTTON click event
    private void UiManager_OnPauseClicked(object sender, EventArgs e)
    {
        OnPauseGame(spawner, timer);
    }

    // PAUSE the game by: PAUSE the Countdown Timer/DISABLE the touch-move player/PAUSE spawning the food
    private void OnPauseGame(IEnumerator foodSpawn, IEnumerator countTimer)
    {
        //isPaused = true;
        StopCoroutine(countTimer);
        StopCoroutine(foodSpawn);
        Player.Instance.isPlaying = false;
    }

    // Received RESTART BUTTON click event
    private void UiManager_OnRestartClicked(object sender, EventArgs e)
    {
        //isPaused = false;
        OnRestartGame();
    }

    // RESTART the game by loading this active scene
    private void OnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Receive the timer change and check the time over or not
    private void GameTimer_OnCountdownTimerChanged(object sender, ITimerChange.OnCountdownTimerChangedEventArgs e)
    {
        if (e.currentTime == 0)
        {
            Debug.Log(lastScore);
            StopCoroutine(spawner);
            StopCoroutine(timer);
            Player.Instance.isPlaying = false;

            if (lastScore <= 0)
            {
                OnGameOver?.Invoke(this, EventArgs.Empty);
                return;
            }

            OnGameEnded?.Invoke(this, new OnGameEndedEventArgs { score = lastScore });
        }
    }

    // Receive the score change and check the score is below 
    private void ScoreChange_OnPlayerScoreChanged(object sender, IScoreChange.OnPlayerScoreChangedEventArgs e)
    {
        lastScore = e.currentPlayerScore;
        //Debug.Log(lastScore);

        if (e.currentPlayerScore < 0)
        {
            //Debug.Log(lastScore);

            StopCoroutine(spawner);
            StopCoroutine(timer);
            Player.Instance.isPlaying = false;
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }
}
