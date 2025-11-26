using UnityEngine;

public class GameOverPlayer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel; 
    [SerializeField] private Canvas gameOverCanvas;    

    public void GameOver()
    {
        Debug.Log("GameOver");

        
        PlayerMove move = GetComponent<PlayerMove>();
        if (move != null)
        {
            move.enabled = false;
        }
        else
        {
            Debug.LogWarning("PlayerMove no encontrado en GameOverPlayer");
        }

        
        if (gameOverCanvas != null && gameOverCanvas.renderMode == RenderMode.WorldSpace)
        {
            Vector3 deathPosition = transform.position;
            gameOverCanvas.transform.position = deathPosition + new Vector3(0, 0, -1); 
            gameOverCanvas.transform.rotation = Quaternion.identity;
            gameOverCanvas.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f); 
        }

        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverPanel no asignado en GameOverPlayer");
        }
    }
}
