using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGameplay()
    {
        // Detener música si existe
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic();
        }

        // Cargar la escena del nivel
        SceneManager.LoadScene("Level01");
    }
}
