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
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        Debug.Log(SceneManager.GetActiveScene().name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // if()
    }
}
