using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {


    #region Properties
    public static GameManager Instance = null;

    [Header("Components Reference")]
    [SerializeField] private GameObject confettiObj = null;


    [Header ("Attributes")]
    List<GameObject> collectedBalls = new List<GameObject>();
    public int ballsRemaining;

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
    #endregion


    public void Lose()
    {
        print("Lostt");
    }

    public void Win()
    {
        print("Win");
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