using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Button playButton, restartButton, continueButton;

    [Header("MENU UI")]
    [SerializeField]
    GameObject menuView;

    [Header("GAMEPLAY UI")]
    [SerializeField]
    GameObject gameplayView;

    [Header("LOST UI")]
    [SerializeField]
    GameObject lostView;

    [Header("WIN UI")]
    [SerializeField]
    GameObject winView;

    void Start()
    {
        Application.targetFrameRate = 60;
        playButton.OnClickAsObservable().Subscribe(_ => {
            MessageBroker.Default.Publish<GamePlayMessage>(new GamePlayMessage(GamePlayMessage.COMMAND.PLAYING));
            menuView.SetActive(false);
            gameplayView.SetActive(true);
        });
        restartButton.OnClickAsObservable().Subscribe(_ => {
            MessageBroker.Default.Publish<GamePlayMessage>(new GamePlayMessage(GamePlayMessage.COMMAND.RESTART));
        });
        continueButton.OnClickAsObservable().Subscribe(_ => {
            MessageBroker.Default.Publish<GamePlayMessage>(new GamePlayMessage(GamePlayMessage.COMMAND.CONTINUE));
        });
    }
}
