using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    private void Start() {
                
    }

  public void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}