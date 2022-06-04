using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSoundManager : MonoBehaviour
{
    private AudioSource AudioSrce;
    [SerializeField]public AudioClip[] soundEffects;
    [SerializeField]public AudioClip[] backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        AudioSrce = this.GetComponent<AudioSource>();
        AudioClip stage0 = Resources.Load<AudioClip>("Audio/BackgroundMusic/stage0");
        backgroundMusic.Add(stage0);
        AudioClip stage0 = Resources.Load<AudioClip>("Audio/BackgroundMusic/stage1");
        backgroundMusic.Add(stage1);
        AudioClip stage0 = Resources.Load<AudioClip>("Audio/BackgroundMusic/stage2");
        backgroundMusic.Add(stage2);
    }

    public void playMusic(AudioClip music){
        AudioSrce.clip = music;
        AudioSrce.Play();
    }
    public void playOnce(AudioClip sound){
        AudioSrce.PlayOneShot(sound);
    }
}
