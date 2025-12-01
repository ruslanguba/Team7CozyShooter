using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void SetMasterVolume(float value) => SetVolume("MasterVolume", value);
    public void SetMusicVolume(float value) => SetVolume("MusicVolume", value);
    public void SetSFXVolume(float value) => SetVolume("SFXVolume", value);

    private void SetVolume(string parameter, float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f;
        audioMixer.SetFloat(parameter, dB);
    }

    public float GetVolume(string parameter)
    {
        if (audioMixer.GetFloat(parameter, out float value))
        {
            return Mathf.Pow(10f, value / 20f);
        }
        return 1f;
    }

}
