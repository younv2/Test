using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioSource AudioSource {  get { return audioSource; } }

    public void Setting(AudioMixerGroup mixerGroup, AudioClip clip,bool isLoop)
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.clip = clip;   
        audioSource.loop = isLoop;

    }
    public void Play()
    {
        
        audioSource.Play();
        StartCoroutine(DestroyWhenEndSound(audioSource.clip.length));
    }

    IEnumerator DestroyWhenEndSound(float time)
    {
        yield return new WaitForSeconds(time);

        SoundManager.instance.soundPlayerList.Remove(this);
        Destroy(this);
    }
}
