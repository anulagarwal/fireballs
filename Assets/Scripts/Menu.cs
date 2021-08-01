using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Button playButton;
    void Start()
    {
        playButton.onClick.AddListener( () => {
            StartCoroutine("LoadGamePlay");
        });
    }

    IEnumerator LoadGamePlay() {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("GamePlay", LoadSceneMode.Single);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
