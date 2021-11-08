using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string blockTag = "Block";
    public static string floorTag = "Floor";

    public GameObject startGamePanel;
    public GameObject gameOverPanel;
    public GameObject gameplayPanel;
    public Image scoreUnit;
    public Image scoreDozens;
    public Image gameOverScoreUnit;
    public Image gameOverScoreDozens;
    public Image highScoreUnit;
    public Image highScoreDozens;
    public AudioClip buttonClip;

    private int _gameScore = 0;
    private int _highScore = 0;
    private int _unitCounter = 0;
    private int _dozenCounter = 0;
    private bool _gameStartState = false;
    private bool _gameStarted = false;
    private bool _gameOverState = false;
    private AudioSource _audioSource;

    [SerializeField] private List<ParallaxObject> parallaxObjects;
    [SerializeField] private ParallaxObject[] blockObstacles;
    [SerializeField] private Sprite[] scoreNumbers;

    public int GameScore
    {
        get { return _gameScore; }
        set { _gameScore = value; }
    }

    public bool GameStartState
    {
        get { return _gameStartState; }
        set { _gameStartState = value; }
    }

    public bool GameOverState
    {
        get { return _gameOverState; }
        set { _gameOverState = value; }
    }

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!_gameStarted)
            GameStart();

        if (_gameOverState)
            GameOver();
    }

    void GameStart()
    {
        if (_gameStartState)
        {
            // Hide UI elements
            startGamePanel.SetActive(false);
            gameplayPanel.SetActive(true);

            // Start blocks movement
            for (int i = 0; i < blockObstacles.Length; i++)
            {
                blockObstacles[i].startMovement = true;
            }

            _gameStarted = true;
        }
    }

    void GameOver()
    {
        // Stop parallax objects movement
        for (int i = 0; i < parallaxObjects.Count; i++)
        {
            parallaxObjects[i].speed = 0;
        }

        CalculateHighScore();

        StartCoroutine(ShowResults());
    }

    public void CalculateScore()
    {
        _unitCounter++;

        if (_gameScore > 9)
        {
            scoreDozens.gameObject.SetActive(true);
            gameOverScoreDozens.gameObject.SetActive(true);
        }

        if (_gameScore % 10 == 0)
            _dozenCounter++;

        if (_unitCounter % 10 == 0)
                _unitCounter = 0;

        gameOverScoreUnit.sprite = scoreUnit.sprite = scoreNumbers[_unitCounter];
        gameOverScoreDozens.sprite = scoreDozens.sprite = scoreNumbers[_dozenCounter];
       
    }

    void CalculateHighScore()
    {
        if (_highScore > 9)
        {
            highScoreDozens.gameObject.SetActive(true);

            highScoreUnit.sprite = scoreNumbers[_highScore % 10];
            highScoreDozens.sprite = scoreNumbers[_highScore / 10];
        }
        else
        {
            highScoreUnit.sprite = scoreNumbers[_highScore % 10];
        }

        if (PlayerPrefs.HasKey("highscore"))
        {
            if (_gameScore > PlayerPrefs.GetInt("highscore"))
            {
                _highScore = _gameScore;
                PlayerPrefs.SetInt("highscore", _highScore);
                PlayerPrefs.Save();
            }

            _highScore = PlayerPrefs.GetInt("highscore");
        }
        else
        {
            _highScore = _gameScore;
            PlayerPrefs.SetInt("highscore", _highScore);
            PlayerPrefs.Save();
        }
    }

    public void RestartLevel()
    {
        StartCoroutine(Restart());
    }

    IEnumerator ShowResults()
    {
        yield return new WaitForSeconds(1f);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    IEnumerator Restart()
    {
        _audioSource.PlayOneShot(buttonClip);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Level_01");
    }

    //void OnGUI()
    //{
    //    //Delete all of the PlayerPrefs settings by pressing this Button
    //    if (GUI.Button(new Rect(100, 200, 200, 60), "Delete"))
    //    {
    //        PlayerPrefs.DeleteAll();
    //    }
    //}
}
