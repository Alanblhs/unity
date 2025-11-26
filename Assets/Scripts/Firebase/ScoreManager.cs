using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int FinalScore { get; private set; }

    public static void CalcularScore(float tiempo, int nivel, int enemigos)
    {
        FinalScore = Mathf.RoundToInt(tiempo * 10 + nivel * 100 + enemigos * 50);
        Debug.Log($"🏅 Puntuación calculada: {FinalScore}");
    }
}
