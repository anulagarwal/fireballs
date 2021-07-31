using UnityEngine;

[CreateAssetMenu(fileName ="Level Data", menuName = "Level/LevelData", order = 1)]
public class Level: ScriptableObject {
    public int levelNo;
    private string levelName;
    public GameObject levelPrefab;

    public int numberOfBalls = 5;
}