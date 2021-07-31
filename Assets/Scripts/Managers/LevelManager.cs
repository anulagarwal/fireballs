using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour
{
    [SerializeField]
    Text levelLbl;

    [SerializeField]
    List<Level> levels;
    private int currentLevelIndex = 0;
    private Level currentLevelData;
    private GameObject levelPrefab;
    private void Start() {
        currentLevelData = levels[currentLevelIndex];

        levelPrefab = Instantiate(currentLevelData.levelPrefab);
        levelPrefab.transform.position = Vector3.zero;
        levelPrefab.transform.rotation = Quaternion.identity;

        levelPrefab.transform.SetParent(transform);

        UpdateLevelInfo();
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
        Destroy(levelPrefab);
        levelPrefab = Instantiate(currentLevelData.levelPrefab);
        levelPrefab.transform.position = Vector3.zero;
        levelPrefab.transform.rotation = Quaternion.identity;

        levelPrefab.transform.SetParent(transform);
        UpdateLevelInfo();
    }
}
