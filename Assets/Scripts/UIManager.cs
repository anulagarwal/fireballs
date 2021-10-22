using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using UniRx;

public class UIManager : MonoBehaviour
{

    #region Properties
    public static UIManager Instance = null;
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

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuUIObj = null;
    [SerializeField] private GameObject gameplayUIObj = null;
    [SerializeField] private GameObject gameOverVictoryUIObj = null;
    [SerializeField] private GameObject gameOverDefeatUIObj = null;

    [Header("Level text")]
    [SerializeField] List<Text> levelTexts;
    [SerializeField] Text finalScore;

    [SerializeField] GameObject awesomeText;

    [Header("Win Bar")]
    [SerializeField] Image inGameBarFill;
    [SerializeField] GameObject rankSystem;


    [SerializeField] float countdownSpeed;
    [SerializeField] GameObject continueButton;
    [SerializeField] Text inGameBetter;

    [SerializeField] GameObject betterText;



    private int tempScore;
    private int targetScore;
    private int bestScore;


    void Start()
    {
        // Application.targetFrameRate = 60;   
        QualitySettings.vSyncCount = 0;    
    }

    private void Update()
    {
        
    }


    #region Public Core Functions
    public void SwitchUIPanel(UIPanelState state, GameOverState gameOverState = GameOverState.None)
    {
        switch (state)
        {
            case UIPanelState.MainMenu:
                mainMenuUIObj.SetActive(true);
                gameplayUIObj.SetActive(false);
                gameOverVictoryUIObj.SetActive(false);
                gameOverDefeatUIObj.SetActive(false);
                break;
            case UIPanelState.Gameplay:
                mainMenuUIObj.SetActive(false);
                gameplayUIObj.SetActive(true);
                gameOverVictoryUIObj.SetActive(false);
                gameOverDefeatUIObj.SetActive(false);
                break;
            case UIPanelState.Victory:
                mainMenuUIObj.SetActive(false);
                gameplayUIObj.SetActive(false);
                gameOverVictoryUIObj.SetActive(true);
                gameOverDefeatUIObj.SetActive(false);               
                break;
            case UIPanelState.Lose:
                mainMenuUIObj.SetActive(false);
                gameplayUIObj.SetActive(false);
                gameOverVictoryUIObj.SetActive(false);
                gameOverDefeatUIObj.SetActive(true);
                break;
        }
    }

    public void UpdateLevelText(int value)
    {
        foreach(Text t in levelTexts)
        {
            t.text = "LEVEL " + value;
        }
    }

    public void UpdateScore(int value)
    {
        finalScore.text = value + " BALLS COLLECTED";
    }
    //Start Score Countdown
    //Stop when final score reaches
    //Simultanerously fill bar

 
  

    public void EnableInGameRank()
    {
        rankSystem.SetActive(true);
        inGameBarFill.gameObject.SetActive(true);
        inGameBetter.gameObject.SetActive(true);
    }
    public void UpdateRankScore(float val)
    {
        float value = val / GameManager.Instance.bestScore;
        inGameBarFill.fillAmount = value;
        inGameBetter.text = Mathf.Min(Mathf.RoundToInt(value * 100), 99) + "%";
    }
    public void SpawnText(Vector3 pos)
    {
        Instantiate(awesomeText, pos, Quaternion.identity);
    }

 
    #endregion
}
