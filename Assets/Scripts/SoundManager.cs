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
    [HideInInspector]public Dictionary<SoundType,List<SoundPlayer>> soundPlayerDic;

    public AudioMixer AudioMixer { get { return audioMixer; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        soundPlayerDic = new Dictionary<SoundType, List<SoundPlayer>>();
    }
    public void SetVolume(SoundType type, float volume)
    {
        audioMixer.SetFloat(type.ToString(), Mathf.Log10(volume) * 20);
    }
    /// <summary>
    /// ���� ����
    /// </summary>
    /// <param name="type">���� Ÿ��</param>
    /// <param name="name">���� Ŭ�� ��</param>
    /// <param name="isLoop">�ݺ� ����</param>
    public void PlaySound(SoundType type, string name, bool isLoop = false)
    {
        if(type==SoundType.BGM && soundPlayerDic.ContainsKey(type) && soundPlayerDic[type].Count >=1)
        {
            return;
        }
        GameObject go = Instantiate(new GameObject(), transform);
        SoundPlayer sp = go.AddComponent<SoundPlayer>();
        AudioMixerGroup mixerGroup = audioMixer.FindMatchingGroups(type.ToString())[0];
        AudioClip clip = audioClipList.Find(x => x.name == name);

        sp.Setting(mixerGroup, clip, isLoop);
        sp.Play();

        if(soundPlayerDic.ContainsKey(type))
        {
            soundPlayerDic[type].Add(sp);
        }
        else
        {
            soundPlayerDic.Add(type,new List<SoundPlayer> { sp });
        }
    }
}
