using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void BeginGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
