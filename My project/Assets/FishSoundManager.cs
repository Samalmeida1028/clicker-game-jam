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
        AudioClip stage1 = Resources.Load<AudioClip>("Audio/BackgroundMusic/");
        backgroundMusic = Resources.LoadAll<AudioClip>("Audio/BackgroundMusic");
    }

    public void playMusic(AudioClip music){
        AudioSrce.clip = music;
        AudioSrce.Play();
    }
    public void playOnce(AudioClip sound){
        AudioSrce.PlayOneShot(sound);
    }
}
