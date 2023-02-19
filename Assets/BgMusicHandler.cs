using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicHandler : MonoBehaviour
{
    public AudioSource audioData;
    public AudioClip mainStart;
    public AudioClip mainRepeat;
    public AudioClip bossRepeat;

    public AudioClip winMusic;
    public static bool bossPlaying = false;
    public bool end = false;

    public static bool firstPass = true;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        if((SceneManager.GetActiveScene().name).Equals("Map")) {
            if(BgMusicHandler.firstPass) {
                audioData.clip = mainStart;
                firstPass = false;
                audioData.Play();
            } else if(BgMusicHandler.bossPlaying) {
                audioData.clip = bossRepeat;
                audioData.Play();
            } else {
                audioData.clip = mainRepeat;
                audioData.Play();
            }
        } else if(end) {
            audioData.clip = winMusic;
            audioData.Play();
        }
         
    }
    // Update is called once per frame
    void Update()
    {
        if(audioData.isPlaying == false) {
            if(end) {
                audioData.clip = winMusic;
                audioData.Play();
            } else if(BgMusicHandler.bossPlaying) {
                audioData.clip = bossRepeat;
                audioData.Play();
            } else {
                audioData.clip = mainRepeat;
                audioData.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(BgMusicHandler.bossPlaying && other.name == "Player") {
            audioData.Stop();
            audioData.clip = mainRepeat;
            audioData.Play();
            BgMusicHandler.bossPlaying = false;
            Debug.Log("Entered Main Area");
        }
    }

    public void TriggerBossRoom() {
        if(!BgMusicHandler.bossPlaying) {
            audioData.Stop();
            audioData.clip = bossRepeat;
            audioData.Play();
        }
    }
}
