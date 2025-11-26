using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;

    public int currentExperience = 0;
    public int maxExperience = 100;
    public int currentLevel = 0;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (ExperienceBar.instance != null)
        {
            ExperienceBar.instance.UpdateExperienceSlider(currentExperience, maxExperience);
            ExperienceBar.instance.SetLevelText(currentLevel);
        }
    }

    void Update()
    {
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;
        currentExperience = Mathf.Clamp(currentExperience, 0, maxExperience);

        Debug.Log($"[ExperienceLevelController] EXP actual: {currentExperience}/{maxExperience}");

        if (ExperienceBar.instance != null)
        {
            ExperienceBar.instance.UpdateExperienceSlider(currentExperience, maxExperience);
        }

        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExperience = 0;

        Debug.Log($"¡Subiste al nivel {currentLevel}!");

        if (ExperienceBar.instance != null)
        {
            ExperienceBar.instance.UpdateExperienceSlider(currentExperience, maxExperience);
            ExperienceBar.instance.SetLevelText(currentLevel);
        }
    }

    
    public static int NivelActual => instance != null ? instance.currentLevel : 0;
}
