using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour {

    AudioSource audioSource;

    public void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Game.Instance.musicVolume;
    }
}
