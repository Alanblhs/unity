using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public float currentHealth;
    public float maxHealth = 9f;

    private bool isDead;

    [Header("UI")]
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Audio")]
    [SerializeField] private AudioSource damageAudio;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        
        if (damageAudio != null)
        {
            damageAudio.Play();
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            gameObject.SetActive(false);

            SurvivalTimer timer = FindObjectOfType<SurvivalTimer>();
            if (timer != null)
            {
                timer.StopTimer();
                Debug.Log("Contador detenido al morir");
            }

            if (gameOverCanvas != null)
            {
                Canvas canvasComponent = gameOverCanvas.GetComponent<Canvas>();
                if (canvasComponent != null && canvasComponent.renderMode == RenderMode.WorldSpace)
                {
                    gameOverCanvas.transform.position = transform.position + new Vector3(0, 0, -1);
                    gameOverCanvas.transform.rotation = Quaternion.identity;
                    gameOverCanvas.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                }

                gameOverCanvas.SetActive(true);
                Debug.Log("Game Over activado desde PlayerHealth");
            }
            else
            {
                Debug.LogWarning("GameOverCanvas no asignado en PlayerHealth");
            }
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
