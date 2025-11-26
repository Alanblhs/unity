using UnityEngine;

public class LevelMusicTrigger : MonoBehaviour
{
    [SerializeField] AudioClip musicaDelNivel;

    void Start()
    {
        MusicManager manager = FindObjectOfType<MusicManager>();
        if (manager != null && musicaDelNivel != null)
        {
            manager.Play(musicaDelNivel); 
        }
        else
        {
            Debug.LogWarning("🎵 No se encontró MusicManager o clip de música del nivel.");
        }
    }
}
