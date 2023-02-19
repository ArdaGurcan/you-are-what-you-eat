using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
  Transform player;
  PlayerMovement playerScript;
  [SerializeField]
  LayerMask mask;
  [SerializeField]
  Vector2 movementVector;
  Rigidbody2D rb;
  Animator anim;
  [SerializeField]
  bool dead = false;
  float speed = 2f;
  [SerializeField]
  float noiseScale = 2f;
  [SerializeField]
  float movementThreshold = 0.1f;
  Vector3 lastPos;


  public float health = 3.0f;

  public Collider2D enemy_collider;
  float unique;

  void Start()
  {
    unique = transform.position.GetHashCode();
    Debug.Log(unique/1_000_000_000);
    player = GameObject.FindGameObjectWithTag("Player").transform;
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    enemy_collider = GetComponent<Collider2D>();

    playerScript = player.GetComponent<PlayerMovement>();
  }

  void FixedUpdate()
  {

    if (!dead && !playerScript.eating)
    {
      RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, 4f, mask);

      movementVector = Vector3.zero;

      if (hit.collider != null && hit.collider.CompareTag("Player"))
      {
        movementVector = player.position - transform.position;
      }
      else
      {
        Vector2 perlinVector = new Vector3(Mathf.PerlinNoise((Time.fixedTime * noiseScale + unique/10000) % (float.MaxValue), 0) * 2 - 1, Mathf.PerlinNoise(0, (Time.fixedTime * noiseScale + unique/10000) % (float.MaxValue)) * 2 - 1);
        if (perlinVector.sqrMagnitude > movementThreshold)
          movementVector = perlinVector;
      }

      rb.MovePosition(transform.position + Vector3.Normalize(movementVector) * Time.fixedDeltaTime * speed / 2);
      Vector2 velocity = (transform.position - lastPos) / Time.fixedDeltaTime;

      anim.SetFloat("MoveX", velocity.x);
      anim.SetFloat("MoveY", velocity.y);
      anim.SetFloat("Speed", velocity.sqrMagnitude);
      lastPos = transform.position;
    }

    if (health <= 0.0)
    {
      Die();
    }

  }

  public void Die()
  {
    enemy_collider.enabled = false;
    rb.isKinematic = true;
    dead = true;
    anim.SetBool("Dead", true);

  }



  void OnHit(float damage)
  {
    Debug.Log("Enemy was hit for " + damage);
    health = health - damage;
  }

}
