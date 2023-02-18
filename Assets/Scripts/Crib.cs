using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crib : MonoBehaviour
{
  public Sprite blood;
  bool nearby = false;
  bool alive = true;
  public int type = 0;
  PlayerMovement player;

  void Start()
  {
    if (type == 0) 
      GetComponent<SpriteRenderer>().color = new Color(69/255f,176/255f,(16*14 + 6)/255f);
    if (type == 1) 
      GetComponent<SpriteRenderer>().color = new Color((16*9+6)/255f,(16*14 + 6)/255f,(16*4+5)/255f);
    if (type == 2) 
      GetComponent<SpriteRenderer>().color = new Color(16*(11 + 11)/255f,(16*3+3)/255f,(16*14 + 6)/255f);
    if (type == 3) 
      GetComponent<SpriteRenderer>().color = new Color(0.9019608f,0.2773975f,0.2705882f);
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      nearby = true;
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
      nearby = false;
  }

  void Update()
  {
    if (!player.eating && alive && nearby && (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)))
    {
      alive = false;
      player.Eat(type);
      transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = blood;
    }
  }
}
