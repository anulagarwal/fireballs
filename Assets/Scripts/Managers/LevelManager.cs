using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour
{
    [SerializeField]
    Text levelLbl;

    [SerializeField]
    List<Level> levels;
    private int currentLevelIndex = 1;
    private Level currentLevelData;
    private GameObject levelPrefab;
    private void Start() {
        currentLevelData = levels[currentLevelIndex];
        UpdateLevelInfo();
        MessageBroker.Default.Receive<GamePlayMessage>()
        .Where(x => x.commandType == GamePlayMessage.COMMAND.RESTART || x.commandType == GamePlayMessage.COMMAND.PLAYING)
        .Subscribe(x => ResetLevel());

        MessageBroker.Default.Receive<GamePlayMessage>()
        .Where(x => x.commandType == GamePlayMessage.COMMAND.CONTINUE)
        .Subscribe(x => ContinueLevel());
    }

    void ContinueLevel() {
        currentLevelIndex++;
        ResetLevel();
    }

    private void UpdateLevelInfo() {
        levelLbl.text = "Level:" + currentLevelData.levelNo.ToString();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetLevel();
        }
    }

    private void ResetLevel () {
        currentLevelData = levels[currentLevelIndex];
        Destroy(levelPrefab);
        levelPrefab = Instantiate(currentLevelData.levelPrefab);
        levelPrefab.transform.position = Vector3.zero;
        levelPrefab.transform.rotation = Quaternion.identity;

        levelPrefab.transform.SetParent(transform);
        UpdateLevelInfo();
    }
}
