using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float swordDamage = 1f;
    public Collider2D swordCollider;
    // Start is called before the first frame update
    void Start()
    {
        if(swordCollider == null) {
            Debug.Log("Sword Collider not set");
        }
        swordCollider.GetComponent<Collider2D>();
        Debug.Log(swordCollider);
    }
    
    /*
    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("Collided with " + col);
         col.collider.SendMessage("OnHit", swordDamage);
    }
    */

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("Collided with " + collider);
        collider.SendMessage("OnHit", swordDamage);
    }
    
}
