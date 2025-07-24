using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();

        }
    }
    public void SetMusicVolume()
    {
        audioMixer.SetFloat("music", Mathf.Log10(slider.value) * 20);
        PlayerPrefs.SetFloat("musicVolume", slider.value);
    }
    void LoadVolume()
    {
        slider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        SetMusicVolume();
    }
}
