using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start() {
        Score = 0;
    }
}
