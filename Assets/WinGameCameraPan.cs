using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameCameraPan : MonoBehaviour
{
    private Vector3 bottom;
    public float speed;

    // Start is called before the first frame update
     void Awake()
    {
        
    PlayerMovement.lastCheckpoint = new Vector3(-2.79f,-7.41f,0);
    }
    void Start()
    {
       bottom = new Vector3(0,-3.8900001f,-10);
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, bottom, (speed/1000f));
    }
}
