using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class settings : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer; // Necesitarás un Audio Mixer en tu proyecto
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        // Asegurarse de que el slider tenga el valor inicial correcto
        float volume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = volume;
        SetVolume(volume);

        // Añadir listener al slider para que llame a SetVolume cuando cambie
        volumeSlider.onValueChanged.AddListener(SetVolume);

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @" + resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        // Si el valor del slider es 0, establecer el volumen a un valor muy bajo
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat("Volume", -80f);  // Silenciar completamente
        }
        else
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        }

        // Guardar el valor del volumen
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
