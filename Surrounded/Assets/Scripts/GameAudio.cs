using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour {
    
    AudioSource audioSource;

    public AudioClip classic;
    public AudioClip frozen;
    public AudioClip easy;
    public AudioClip hard;
    public AudioClip cluster;
    public AudioClip twohit;
    public AudioClip teleport;
    public AudioClip dodge;
    public AudioClip invisible;

    void Awake () {
        audioSource = GetComponent<AudioSource>();
        switch (Game.Instance.gameMode) {
            case GameMode.Frozen:
                audioSource.clip = frozen;
                break;
            case GameMode.Easy:
                audioSource.clip = easy;
                break;
            case GameMode.Hard:
                audioSource.clip = hard;
                break;
            case GameMode.Cluster:
                audioSource.clip = cluster;
                break;
            case GameMode.TwoHit:
                audioSource.clip = twohit;
                break;
            case GameMode.Teleport:
                audioSource.clip = teleport;
                break;
            case GameMode.Dodge:
                audioSource.clip = dodge;
                break;
            case GameMode.Invisible:
                audioSource.clip = invisible;
                break;
            default:
                audioSource.clip = classic;
                break;
        }
        audioSource.Play();
	}

    public void Pause() {
        audioSource.Pause();
    }

    public void Resume() {
        audioSource.UnPause();
    }

}
