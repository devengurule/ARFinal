using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
