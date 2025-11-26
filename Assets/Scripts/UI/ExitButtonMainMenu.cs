using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonMainMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
