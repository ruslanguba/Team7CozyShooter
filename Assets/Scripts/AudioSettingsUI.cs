using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("Sliders")]
    //[SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // Загружаем текущие громкости в UI
        //masterSlider.value = AudioManager.Instance.GetVolume("MasterVolume");
        musicSlider.value = AudioManager.Instance.GetVolume("MusicVolume");
        sfxSlider.value = AudioManager.Instance.GetVolume("SFXVolume");

        // Подписываем события
        //masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }
}
