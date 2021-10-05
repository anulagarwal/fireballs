using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {

    #region Properties
    public static GameManager Instance = null;

    [Header("Components Reference")]
    [SerializeField] private GameObject confettiObj = null;

    [Header ("Attributes")]
    [SerializeField] public int numberOfBalls;
    [SerializeField] public int maxLevels;
    [SerializeField] public int requiredBalls;


    //Internal values
    [SerializeField] List<GameObject> collectedBalls = new List<GameObject>();
    [SerializeField] private int ballsRemaining;
    private int currentLevel;
    public bool isGameOn;
    public bool isWon;


    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("level", 1);
        UIManager.Instance.UpdateLevelText(currentLevel);
        BucketController.Instance.enabled = false;
        print(currentLevel);
    }
    #endregion

    private void Update()
    {
        if (!isGameOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartLevel();
            }
        }
    }


    public void StartLevel()
    {
        isGameOn = true;
        UIManager.Instance.SwitchUIPanel(UIPanelState.Gameplay);
        TinySauce.OnGameStarted(levelNumber: "" + currentLevel);
        BucketController.Instance.enabled = true;
        BucketController.Instance.SpawnBall();

    }

    public void Lose()
    {
        //isGameOn = false;
        Invoke("ShowLoseUI", 2f);
        TinySauce.OnGameFinished(false, 0);
    }

    public void Win()
    {
        //isGameOn = false;
        Invoke("ShowWinUI", 2f);
        confettiObj.SetActive(true);
        TinySauce.OnGameFinished(true, 0);
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
    }

    public void ChangeLevel()
    {
        if (currentLevel > maxLevels)
        {
            int newId = currentLevel % maxLevels;
            if(newId == 0)
            {
                newId = maxLevels;
            }
            SceneManager.LoadScene("Level " + (newId));
        }
        else
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
    }
    public void ShowWinUI()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.Victory);
        UIManager.Instance.UpdateScore(collectedBalls.Count);

    }

    public void ShowLoseUI()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.Lose);
    }

    public void AddBallToBasket(GameObject g)
    {
        collectedBalls.Add(g);
        ReduceRemainingBalls(1);
    }

    public void SetRemainingBalls(int value)
    {
        ballsRemaining = value;
    }
    public void AddRemainingBalls(int value)
    {
        ballsRemaining += value;
    }

    public void ReduceRemainingBalls(int value)
    {
        ballsRemaining -= value;

        if (ballsRemaining <= 0 &&  collectedBalls.Count < requiredBalls)
        {
            Lose();
        }
        else if(ballsRemaining <= 0 && collectedBalls.Count >= 0  && collectedBalls.Count > requiredBalls)
        {
            if (!isWon)
            {
                Win();
                isWon = true;
            }
        }
    }

}