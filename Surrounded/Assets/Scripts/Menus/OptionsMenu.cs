using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour {

    public Slider volumeSlider;

    public TMP_Dropdown resolutionDropdown;

	public TMP_Dropdown graphicsQualityDropdown;

    public Toggle fullscreenToggle;
    public Toggle vSyncToggle;

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

    public void SetVSync(bool vSyncOn)
    {
        Game.SetVSync(vSyncOn);
        SaveLoad.Save();
    }

    public void SetResolution(int resolutionIndex) {
        Game.SetResolution(resolutionIndex);
        SaveLoad.Save();
    }

    void Awake() {
        volumeSlider.value = Game.Instance.musicVolume;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (Resolution r in Screen.resolutions) options.Add(r.ToString()); // options.Add(r.width + " x " + r.height);

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = Game.GetResolutionIndex();
        resolutionDropdown.RefreshShownValue();

        graphicsQualityDropdown.value = Game.Instance.quality;
        graphicsQualityDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Game.Instance.fullScreen;
        vSyncToggle.isOn = Game.Instance.vSyncOn;
    }
    
    public void Reset() {
        Game.Reset();
        SaveLoad.Save();
    }
}
