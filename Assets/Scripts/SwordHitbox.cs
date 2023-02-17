using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
  public float swordDamage = 1f;
  public Collider2D swordCollider;
  bool attacking = false;
  float attackDuration = 0.2f;
  float attackTimer = 0f;
  // Start is called before the first frame update
  void Start()
  {
    if (swordCollider == null)
    {
      Debug.Log("Sword Collider not set");
    }
    swordCollider.GetComponent<Collider2D>();
    Debug.Log(swordCollider);
  }


  void Update()
  {

    if (!attacking && Input.GetKeyDown(KeyCode.X))
    {
      attacking = true;
      attackTimer = attackDuration;
    }

    if (attacking && attackTimer > 0)
    {
      attackTimer -= Time.deltaTime;
    }
    else if (attacking)
    {
      attacking = false;
    }
  }
  /*
  void OnCollisionEnter2D(Collision2D col) {
      Debug.Log("Collided with " + col);
       col.collider.SendMessage("OnHit", swordDamage);
  }
  */

  void OnTriggerStay2D(Collider2D collider)
  {
    if (attacking && collider.gameObject.layer == 3)
    {
      attacking = false;

      Debug.Log("Collided with " + collider);
      collider.SendMessage("OnHit", swordDamage);

    }
  }

}
