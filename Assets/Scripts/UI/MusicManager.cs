using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip musicOnStart;
    [SerializeField] float timeToSwitch = 1f;

    private AudioSource audioSource;
    private float targetVolume = 1f;

    private void Awake()
    {
        
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); 
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (musicOnStart != null)
        {
            audioSource.clip = musicOnStart;
            audioSource.volume = targetVolume;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void Play(AudioClip newClip)
    {
        if (newClip == null) return;
        StartCoroutine(SwitchMusic(newClip));
    }

    public void StopMusic()
    {
        StartCoroutine(FadeOutAndStop());
    }

    private IEnumerator SwitchMusic(AudioClip newClip)
    {
        float startVolume = audioSource.volume;

        
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / timeToSwitch;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.loop = true;
        audioSource.Play();

        
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += Time.deltaTime / timeToSwitch;
            yield return null;
        }

        audioSource.volume = startVolume;
    }

    private IEnumerator FadeOutAndStop()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / timeToSwitch;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
