using UnityEngine;
using TMPro;

public class SurvivalTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private bool isCounting = true;

    public static float TiempoFinal { get; private set; }

    void Update()
    {
        if (!isCounting) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void StopTimer()
    {
        isCounting = false;
        TiempoFinal = elapsedTime;
        Debug.Log($"⏱️ Tiempo final registrado: {TiempoFinal}");
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
