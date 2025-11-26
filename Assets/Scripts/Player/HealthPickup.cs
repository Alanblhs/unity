using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource pickupAudio;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerHealth.instance != null)
        {
            if (PlayerHealth.instance.currentHealth < PlayerHealth.instance.maxHealth)
            {
                PlayerHealth.instance.Heal(healAmount);

                
                if (pickupAudio != null)
                {
                    pickupAudio.Play();
                }

                
                if (pickupAudio != null && pickupAudio.clip != null)
                {
                    Destroy(gameObject, pickupAudio.clip.length);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
