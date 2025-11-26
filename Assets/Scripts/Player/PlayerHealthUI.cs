using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image[] heartImages;         
    public Sprite heartFull;            
    public Sprite heartEmpty;          

    void Update()
    {
        if (PlayerHealth.instance == null) return;

        int current = Mathf.RoundToInt(PlayerHealth.instance.currentHealth);

        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < current ? heartFull : heartEmpty;
        }
    }
}
