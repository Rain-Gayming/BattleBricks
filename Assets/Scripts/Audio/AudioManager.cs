using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<Source> audioSources;
    private void Awake() {
        instance = this;
    }
    public void PlayAudio(string audioName, string audioSource, bool loops)
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if(audioSources[i].sourceName == audioSource){
                for (int a = 0; a < audioSources[i].pack.clips.Count; a++)
                {
                    if(audioSources[i].pack.clips[i].clipName == audioName){
                        audioSources[i].source.clip = audioSources[i].pack.clips[i].clip;
                        audioSources[i].source.Play();
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class SoundEffect
{
    public string clipName;
    public AudioClip clip;
    public AudioSource source;
}
[System.Serializable]
public class Source
{
    public string sourceName;
    public AudioMixerGroup mixerGroup;
    public AudioSource source;
    public AudioPack pack;
}