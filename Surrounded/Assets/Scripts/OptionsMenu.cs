using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour {

    public Slider volumeSlider;

    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;

	public TMP_Dropdown graphicsQualityDropdown;

	public void ChangeVolume(float volume) {
        Game.Instance.musicVolume = volume;
        Camera.main.GetComponent<MusicVolume>().Start();
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void Start() {
        volumeSlider.value = Game.Instance.musicVolume;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        foreach (Resolution r in resolutions) {
            if (r.height == Screen.currentResolution.height && r.width == Screen.currentResolution.width) currentResolutionIndex = options.Count;
            options.Add(r.width + " x " + r.height);

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();
        graphicsQualityDropdown.RefreshShownValue();

    }

    public void Reset() {
        Game.Reset();
        SaveLoad.Save();
    }
}
