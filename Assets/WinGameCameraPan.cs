using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGameCameraPan : MonoBehaviour
{
    private Vector3 bottom;
    public float speed;
    Image panel;
    Image img;
PlayerMovement player;
    // Start is called before the first frame update
     void Awake()
    {
        
    PlayerMovement.lastCheckpoint = new Vector3(-2.79f,-7.41f,0);
    }
    void Start()
    {panel = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    Debug.Log(transform.GetChild(0).GetChild(0));
    img = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
       bottom = new Vector3(0,-3.8900001f,-10);
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, bottom, (speed/1000f));
        if (player.dead) {
            if (panel.color.a < 0.7f)
            panel.color += new Color(0,0,0,Time.deltaTime/3);
            if (img.color.a < 1f)
            img.color += new Color(0,0,0,Time.deltaTime/3);
        }
    }
}
