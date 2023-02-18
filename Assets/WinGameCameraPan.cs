using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameCameraPan : MonoBehaviour
{
    private Vector3 bottom;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
       bottom = new Vector3(0f, -0.598f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, bottom, (float)(speed/1000));
    }
}
