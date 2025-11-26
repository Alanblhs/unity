using Firebase;
using Firebase.Database;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public string usuario;
    public int score;
    public float tiempo;
    public int nivel;
    public int enemigos;
}

public class FirebaseManager : MonoBehaviour
{
    private static DatabaseReference dbRef;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("🔥 Firebase conectado");
            }
            else
            {
                Debug.LogError("❌ Firebase no disponible: " + task.Result);
            }
        });
    }

    public static void GuardarScore(string usuario, int score, float tiempo, int nivel, int enemigos)
    {
        if (dbRef == null)
        {
            Debug.LogError("❌ dbRef no está inicializado. Espera a que Firebase se conecte.");
            return;
        }

        if (string.IsNullOrEmpty(usuario))
        {
            Debug.LogError("❌ El nombre de usuario está vacío. No se puede guardar el score.");
            return;
        }

        ScoreData datos = new ScoreData
        {
            usuario = usuario,
            score = score,
            tiempo = tiempo,
            nivel = nivel,
            enemigos = enemigos
        };

        string json = JsonUtility.ToJson(datos);
        Debug.Log("📦 JSON generado: " + json);

        DatabaseReference scoreRef = dbRef.Child("scores").Push();
        scoreRef.SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("✅ Score guardado correctamente en: " + scoreRef.ToString());
            }
            else
            {
                Debug.LogError("❌ Error al guardar score: " + task.Exception);
            }
        });
    }
}
