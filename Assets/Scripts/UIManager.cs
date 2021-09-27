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


    void Start()
    {
        Application.targetFrameRate = 60;       
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
    #endregion
}
