using UnityEngine.SceneManagement;
using UnityEngine;

public class DownBar : MonoBehaviour
{
    public void OpenScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
