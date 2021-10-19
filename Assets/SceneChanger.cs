using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    int maxLevels;
    int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        maxLevels = 11;
        currentLevel = PlayerPrefs.GetInt("level", 1);
        if (currentLevel > maxLevels)
        {
            int newId = currentLevel % maxLevels;
            if (newId == 0)
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

    // Update is called once per frame
    void Update()
    {
        
    }

}
