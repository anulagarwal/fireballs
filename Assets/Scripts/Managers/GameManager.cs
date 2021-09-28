using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {


    #region Properties
    public static GameManager Instance = null;

    [Header("Components Reference")]
    [SerializeField] private GameObject confettiObj = null;


    [Header ("Attributes")]
    public int ballsRemaining;
    public bool isGameOn;
    int currentLevel;
    public int numberOfBalls;
    List<GameObject> collectedBalls = new List<GameObject>();

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
    }
    #endregion




    public void StartLevel()
    {
        isGameOn = true;
        UIManager.Instance.SwitchUIPanel(UIPanelState.Gameplay);
    }

    public void Lose()
    {
        isGameOn = false;
        UIManager.Instance.SwitchUIPanel(UIPanelState.Lose);

    }

    public void Win()
    {
        isGameOn = false;
        UIManager.Instance.SwitchUIPanel(UIPanelState.Victory);

    }

    public void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
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
        if(ballsRemaining <= 0 && collectedBalls.Count <=0)
        {
            Lose();
        }
        else if(ballsRemaining <=0 && collectedBalls.Count >0)
        {
            Win();
        }
    }

}