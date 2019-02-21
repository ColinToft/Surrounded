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

    public Toggle fullscreenToggle;

	public void ChangeVolume(float volume) {
        Game.SetMusicVolume(volume);
        SaveLoad.Save();
    }

    public void SetQuality(int qualityIndex) {
        Game.SetQuality(qualityIndex);
        SaveLoad.Save();
    }

    public void SetFullscreen(bool isFullscreen) {
        Game.SetFullScreen(isFullscreen);
        SaveLoad.Save();
    }

    public void SetResolution(int resolutionIndex) {
        Game.SetResolution(resolutionIndex);
        SaveLoad.Save();
    }

    void Awake() {
        volumeSlider.value = Game.Instance.musicVolume;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        foreach (Resolution r in resolutions)
        {
            if (r.height == Screen.currentResolution.height && r.width == Screen.currentResolution.width) currentResolutionIndex = options.Count;
            options.Add(r.width + " x " + r.height);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = Game.Instance.resolution;
        resolutionDropdown.RefreshShownValue();

        graphicsQualityDropdown.value = Game.Instance.quality;
        graphicsQualityDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Game.Instance.fullScreen;
    }

    void Start() {

    }

    public void Reset() {
        Game.Reset();
        SaveLoad.Save();
    }
}
