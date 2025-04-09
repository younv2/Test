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
    public GameObject CardShadow;
    void Start()
    {
        CardShadow.SetActive(false);

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

    void OnEnable()
    {
        if (CardShadow != null)
            CardShadow.SetActive(false); // ÆË¾÷ ÄÑÁú ¶§ ±×¸²ÀÚ ²¨Áü
    }

    void OnDisable()
    {
        if (CardShadow != null)
            CardShadow.SetActive(true); // ÆË¾÷ ²¨Áú ¶§ ±×¸²ÀÚ ÄÑÁü
    }
}
