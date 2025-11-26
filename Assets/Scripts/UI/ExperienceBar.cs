using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public static ExperienceBar instance;

    [SerializeField] Slider slider;
    [SerializeField] TMPro.TextMeshProUGUI levelText;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Ya existe una instancia de ExperienceBar");
            Destroy(gameObject);
        }
    }

    public void UpdateExperienceSlider(int current, int target)
    {
        slider.maxValue = target;
        slider.value = current;

        
        Debug.Log($"[ExperienceBar] Slider actualizado: {current}/{target}");
    }

    public void SetLevelText(int level)
    {
        levelText.text = "LEVEL:" + level.ToString();
    }
}
