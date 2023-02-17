using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crib : MonoBehaviour
{
  public Sprite blood;
  bool nearby = false;
  bool alive = true;
  PlayerMovement player;


  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      nearby = true;
      player = other.GetComponent<PlayerMovement>();
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
      nearby = true;
  }

  void Update()
  {
    if (alive && nearby && (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)))
    {
      alive = false;
      player.Eat();
      transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = blood;
    }
  }
}
