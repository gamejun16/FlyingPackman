using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /*****
     * 
     * 오디오 소스를 저장하고, 오디오 소스를 제어하는 함수를 저장하는 스크립트
     * 
     * */

    public enum player { COIN, DAMAGED, HEART, }
    public enum gear { SHOT,}
    public enum creature { BOOM, BOOM2, BARRIER, BOSS_INTRO, DANGER_DROP }
    public enum other { LASER, }
    public enum bgm { NORMAL,}

    public static SoundManager soundManager;
    
    [SerializeField] AudioClip[] playerAudioClip;
    [SerializeField] AudioClip[] gearAudioClip;
    [SerializeField] AudioClip[] creatureAudioClip;
    [SerializeField] AudioClip[] otherAudioClip;
    [SerializeField] AudioClip[] bgmAudioClip;
    

    private void Awake()
    {
        soundManager = this;
    }

    public void playPlayerSound(int idx, AudioSource a)
    {
        a.clip = playerAudioClip[idx];
        a.Play();
    }
    public void playGearSound(int idx, AudioSource a)
    {
        a.clip = gearAudioClip[idx];
        a.Play();
    }
    public void playCreatureSound(int idx, AudioSource a)
    {
        a.clip = creatureAudioClip[idx];
        a.Play();
    }
    public void playOtherSound(int idx, AudioSource a)
    {
        a.clip = otherAudioClip[idx];
        a.Play();
    }
    public void playBgmSound(int idx, AudioSource a)
    {
        a.clip = bgmAudioClip[idx];
        a.Play();
    }
    
}
