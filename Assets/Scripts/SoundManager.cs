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
    /// ���� ����
    /// </summary>
    /// <param name="type">���� Ÿ��</param>
    /// <param name="name">���� Ŭ�� ��</param>
    /// <param name="isLoop">�ݺ� ����</param>
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
