using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene(1);       
    }
}
