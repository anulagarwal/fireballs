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
    public GameState currentState;


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
        maxLevels = 11;
        UpdateState(GameState.Main);
       
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
        UpdateState(GameState.Game);
    }

    public void Lose()
    {
        UpdateState(GameState.Lose);
    }

    public void Win()
    {
        UpdateState(GameState.Win);
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

    public void UpdateState(GameState state)
    {
        currentState = state;
        switch (state)
        {
            case GameState.Main:
                currentLevel = PlayerPrefs.GetInt("level", 1);
                UIManager.Instance.UpdateLevelText(currentLevel);
                ballsRemaining = numberOfBalls;
                isGameOn = false;
                
                break;

            case GameState.Game:
                UIManager.Instance.SwitchUIPanel(UIPanelState.Gameplay);
                TinySauce.OnGameStarted(levelNumber: "" + currentLevel);
                BucketController.Instance.enabled = true;
                break;
            case GameState.Win:
                confettiObj.SetActive(true);
                Invoke("ShowWinUI", 2f);
                TinySauce.OnGameFinished(true, 0);

                break;
            case GameState.Lose:
                Invoke("ShowLoseUI", 2f);
                TinySauce.OnGameFinished(false, 0);

                break;

        }
    }

    public GameState GetCurrentState()
    {
        return currentState;
    }

    public void ShowLoseUI()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.Lose);
    }

    public void AddBallToBasket(GameObject g)
    {
        if(!collectedBalls.Contains(g))
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
        if (GameObject.FindGameObjectsWithTag("Ball") != null)
        {
            if (GameObject.FindGameObjectsWithTag("Ball").Length <= 3 && collectedBalls.Count < requiredBalls-2)
            {
                Lose();
            }
            else if (GameObject.FindGameObjectsWithTag("Ball").Length <= 3 &&  collectedBalls.Count >= requiredBalls && LockHandler.Instance==null)
            {
                if (!isWon)
                {
                    Win();
                    isWon = true;
                }
            }
        }
    }

}