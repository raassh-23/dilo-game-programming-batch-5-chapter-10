using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: GameManager not Found");
                }
            }

            return _instance;
        }
    }

    [SerializeField]
    private Text ScoreText;

    private int _score;

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            ScoreText.text = "Score: " + _score;
        }
    }

    [SerializeField]
    private float gameTime;
    [SerializeField]
    private Text TimeText;

    private float _timer;

    public float Timer
    {
        get
        {
            return _timer;
        }

        set
        {
            _timer = value;
            TimeText.text = _timer.ToString("0");
        }
    }

    [SerializeField]
    private GameObject ballGameObject;

    public Ball Ball
    {
        get
        {
            return ballGameObject.GetComponent<Ball>();
        }
    }

    [SerializeField]
    private Text FinalScoreText;

    [SerializeField]
    private Text HighScoreText;

    [SerializeField]
    private GameObject InGamePanel;

    [SerializeField]
    private GameObject GameOverPanel;

    [SerializeField]
    private Text PowerUpText;

    [SerializeField]
    private AudioClip GameOverSfx;

    public bool IsGameOver { get; set; }

    private AudioSource audioSource;

    private void Start()
    {
        Score = 0;
        Timer = gameTime;
        IsGameOver = false;
        PowerUpText.canvasRenderer.SetAlpha(0);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (IsGameOver)
        {
            return;
        }

        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        IsGameOver = true;
        PlayAudio(GameOverSfx);

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (Score > highScore)
        {
            highScore = Score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        ballGameObject.SetActive(false);
        GameOverPanel.SetActive(true);
        InGamePanel.SetActive(false);

        FinalScoreText.text = "Your Score: " + Score;
        HighScoreText.text = "High Score: " + highScore;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SetPowerUpText(string text)
    {
        PowerUpText.text = text;
        PowerUpText.canvasRenderer.SetAlpha(1);
        PowerUpText.CrossFadeAlpha(0, 1f, false);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
