using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    public Slider MasterVolumeSlider;
    public Slider BGMVolumeSlider;
    public Slider SFXVolumeSlider;
    public Button closeBtn;
    void Start()
    {
        closeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        MasterVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.instance.SetVolume(SoundType.Master,value);
        });
        BGMVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.instance.SetVolume(SoundType.BGM, value);
        });
        SFXVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.instance.SetVolume(SoundType.SFX, value);
        });
    }
}
