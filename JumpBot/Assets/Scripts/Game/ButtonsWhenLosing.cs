using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsWhenLosing : MonoBehaviour
{
    public void ButtonRestart()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
