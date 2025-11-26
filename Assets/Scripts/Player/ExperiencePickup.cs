using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    public int expValue;

    [Header("Audio")]
    [SerializeField] private AudioSource pickupAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if (pickupAudio != null)
            {
                pickupAudio.Play();
            }

            ExperienceLevelController.instance.GetExp(expValue);

            if (ExperienceBar.instance != null)
            {
                ExperienceBar.instance.UpdateExperienceSlider(
                    ExperienceLevelController.instance.currentExperience,
                    ExperienceLevelController.instance.maxExperience
                );
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
