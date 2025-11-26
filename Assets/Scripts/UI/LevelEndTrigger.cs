using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    public GameObject victoryCanvas;   
    private GameObject enemyFinal;
    private bool levelEnded = false;

    void Start()
    {
        Time.timeScale = 1f;
        victoryCanvas.SetActive(false); 
    }

    void Update()
    {
        if (!levelEnded)
        {
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                if (enemy.name.Contains("BossMrityuzz"))
                {
                    enemyFinal = enemy;
                    break;
                }
            }

            
            if (enemyFinal != null)
            {
                EnemyMove move = enemyFinal.GetComponent<EnemyMove>();
                if (move != null)
                {
                    Debug.Log("Estado del jefe: isDead = " + move.isDead); 

                    if (move.isDead)
                    {
                        levelEnded = true;

                        victoryCanvas.SetActive(true); 
                        Debug.Log("¡Victoria alcanzada! Boss eliminado.");

                        
                        GameObject[] enemigosRestantes = GameObject.FindGameObjectsWithTag("Enemy");
                        foreach (GameObject enemigo in enemigosRestantes)
                        {
                            Destroy(enemigo);
                        }

                        Time.timeScale = 0f;
                    }
                }
            }
        }
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
