using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    Master,
    BGM,
    SFX
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private List<AudioClip> audioClipList;
    [HideInInspector]public List<SoundPlayer> soundPlayerList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetVolume(SoundType type, float volume)
    {
        audioMixer.SetFloat(type.ToString(), volume);
    }
    /// <summary>
    /// 음악 실행
    /// </summary>
    /// <param name="type">사운드 타입</param>
    /// <param name="name">사운드 클립 명</param>
    /// <param name="isLoop">반복 여부</param>
    public void PlaySound(SoundType type, string name, bool isLoop = false)
    {
        GameObject go = Instantiate(new GameObject(), transform);
        SoundPlayer sp = go.AddComponent<SoundPlayer>();
        AudioMixerGroup mixerGroup = audioMixer.FindMatchingGroups(type.ToString())[0];
        AudioClip clip = audioClipList.Find(x => x.name == name);

        sp.Setting(mixerGroup, clip, isLoop);
        sp.Play();

        soundPlayerList.Add(sp);
    }
}
