using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D rb;
  Animator anim;
  SpriteRenderer sr;
  [SerializeField]
  float speed = 5f;

  [SerializeField]
  float age = 0;
  [SerializeField]
  bool dead = false;

  int spriteAge;
  Dictionary<string, Sprite> spritesheet;

  Vector3 inputVector;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    sr = GetComponent<SpriteRenderer>();
    LoadSpritesheet();
  }

  void Update()
  {
    if (!dead)
    {
      inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
      anim.SetFloat("Horizontal", inputVector.x);
      anim.SetFloat("Vertical", inputVector.y);
      anim.SetFloat("Speed", inputVector.sqrMagnitude);
    }
  }

  void FixedUpdate()
  {
    if (!dead)
    {
      age += Time.fixedDeltaTime / 10f;
      rb.MovePosition(transform.position + Vector3.Normalize  (inputVector) * Time.deltaTime * speed);
    }
  }

  void LateUpdate()
  {
    if (!dead)
    {
      if (Mathf.FloorToInt(age) != spriteAge)
      {
        if (age >= 3f)
        {
          Die();

        }
        else
          LoadSpritesheet();
      }
      speed = 5.5f - 0.95f * age - 0.25f * age * age;

    }
    sr.sprite = spritesheet[sr.sprite.name];
  }

  void LoadSpritesheet()
  {
    var sprites = Resources.LoadAll<Sprite>("age" + Mathf.FloorToInt(age));

    spritesheet = sprites.ToDictionary(x => x.name, x => x);
    spriteAge = Mathf.FloorToInt(age);
  }


  public void Die()
  {
    dead = true;
    anim.SetBool("Dead", true);
  }
}
