using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleVolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("masterVolume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
        UpdateVolumeText(savedVolume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("masterVolume", volume);
        UpdateVolumeText(volume);
    }

    void UpdateVolumeText(float volume)
    {
        int percentage = Mathf.RoundToInt(volume * 100f);
        volumeText.text = percentage.ToString();
    }
}
