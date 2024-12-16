using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadInvestingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("PORTFOLIO");
    }
}
