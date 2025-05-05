using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;         // 音频剪辑的名称
    public AudioClip clip;      // 音频剪辑
    [Range(0f, 1f)]
    public float volume = 0.7f; // 音量大小
}

public class AudioManager : Singleton<AudioManager>
{
    // 定义音乐和音效的Sound数组
    public Sound[] musicSounds, sfxSounds;

    // 音乐和音效的AudioSource
    public AudioSource musicSource, sfxSource;
    public void StopMusic()
    {
        if (musicSource == null)
        {
            Debug.LogError("音乐AudioSource未初始化！");
            return;
        }

        if (musicSource.isPlaying)
        {
            musicSource.Stop();
            musicSource.clip = null; // 清除当前音频剪辑
        }
    }

    // 播放音乐的方法，参数为音乐名称
    public void PlayMusic(string name)
    {
        // 从音乐Sounds数组中找到名字匹配的Sound对象
        Sound s = Array.Find(musicSounds, x => x.name == name);
        // 如果找不到对应的Sound，输出错误信息
        if (s == null || s.clip == null)
        {
            Debug.LogError("未找到音乐：" + name);
            return;
        }
        // 否则将音乐源的clip设置为对应Sound的clip并播放
        musicSource.clip = s.clip;
        musicSource.volume = s.volume;
        musicSource.Play();
    }

    // 播放音效的方法，参数为音效名称
    public void PlaySFX(string name)
    {
        // 从音效Sounds数组中找到名字匹配的Sound对象
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        // 如果找不到对应的Sound，输出错误信息
        if (s == null || s.clip == null)
        {
            Debug.LogError("未找到音效：" + name);
            return;
        }
        // 否则播放对应Sound的clip
        sfxSource.PlayOneShot(s.clip, s.volume);
    }

    // 切换音乐的静音状态
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    // 切换音效的静音状态
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    // 设置音乐音量的方法，参数为音量值
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // 设置音效音量的方法，参数为音量值
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}