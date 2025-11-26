using UnityEngine;
using TMPro;

public class ScoreUploader : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;

    public void GuardarScore()
    {
        string username = usernameInput.text;
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogWarning("⚠️ Debes ingresar un nombre de usuario.");
            return;
        }

        
        float tiempo = SurvivalTimer.TiempoFinal;
        int nivel = ExperienceLevelController.NivelActual;
        int enemigos = EnemySpawner.EnemigosMuertos;

        
        ScoreManager.CalcularScore(tiempo, nivel, enemigos);

        
        FirebaseManager.GuardarScore(username, ScoreManager.FinalScore, tiempo, nivel, enemigos);
    }
}
