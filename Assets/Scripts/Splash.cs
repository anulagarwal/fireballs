using UnityEngine;
using UnityEngine.SceneManagement;
public class Splash : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadGamePlay", 1f);
    }

    void LoadGamePlay() {
        SceneManager.LoadScene(1);
    }
}
