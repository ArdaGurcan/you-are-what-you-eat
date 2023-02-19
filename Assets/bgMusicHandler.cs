using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bgMusicHandler : MonoBehaviour
{
    public AudioSource audioData;
    public AudioClip mainStart;
    public AudioClip mainRepeat;
    public AudioClip bossRepeat;

    public AudioClip winMusic;
    public bool bossPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        if((SceneManager.GetActiveScene().name).Equals("Map")) {
            audioData.clip = mainStart;
            audioData.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(audioData.isPlaying == false) {
            if(bossPlaying) {
                audioData.clip = bossRepeat;
                audioData.Play();
            } else {
                audioData.clip = mainRepeat;
                audioData.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(bossPlaying) {
            audioData.Stop();
            audioData.clip = mainRepeat;
            audioData.Play();
            bossPlaying = false;
        }


    }

    public void TriggerBossRoom() {
        audioData.Stop();
        audioData.clip = bossRepeat;
        audioData.Play();
    }
}
