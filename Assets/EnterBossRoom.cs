using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossRoom : MonoBehaviour
{
    public BgMusicHandler music;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player") {
            music.TriggerBossRoom();
            BgMusicHandler.bossPlaying = true;
        }
    }
 
    private void OnTriggerStay2D(Collider2D other) {
        if(other.name == "Player") {
            music.TriggerBossRoom();
            BgMusicHandler.bossPlaying = true;
            Debug.Log("In the Boss Room");
        }
    }
}
