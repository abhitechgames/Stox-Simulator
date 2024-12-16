using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NavButton : MonoBehaviour
{
    public TMP_Text myText;

    public GameObject Image;

    public Color color;

    void Start()
    {
        if(myText.text == SceneManager.GetActiveScene().name)
        {
            myText.color = color;
            Image.SetActive(true);
        }
        else
        {
            Image.SetActive(false);
        }
    }

    public void ButtonClicked()
    {
        SceneManager.LoadScene(myText.text);
        myText.color = color;
        Image.SetActive(true);
    }
}
