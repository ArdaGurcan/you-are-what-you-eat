using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D rb;
  Animator anim;
  SpriteRenderer sr;
  [SerializeField]
  Image image;

  public Animator eatingAnimation;
  [SerializeField]
  float speed = 5f;

  public GameObject swordHitbox;
  Collider2D swordCollider;

  [SerializeField]
  float age = 0;
  [SerializeField]
  public bool dead = false;

  public bool eating = false;

  int spriteAge;
  Dictionary<string, Sprite> spritesheetMovement;
  Dictionary<string, Sprite> spritesheetEating;

  Vector3 inputVector;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    sr = GetComponent<SpriteRenderer>();
    LoadSpritesheet();

    swordCollider = swordHitbox.GetComponent<Collider2D>();
  }

  void Update()
  {
    if (!dead && !eating)
    {
      inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
      anim.SetFloat("Horizontal", inputVector.x);
      anim.SetFloat("Vertical", inputVector.y);
      anim.SetFloat("Speed", inputVector.sqrMagnitude);
    }

  }

  void FixedUpdate()
  {
    if (!dead && !eating)
    {
      age += Time.fixedDeltaTime / 3f;
      rb.MovePosition(transform.position + Vector3.Normalize(inputVector) * Time.fixedDeltaTime * speed);
    }
    
    if(dead)
    {
      rb.isKinematic = true;
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
    sr.sprite = spritesheetMovement[sr.sprite.name];
    // Debug.Log(image.sprite.name);
    image.sprite = spritesheetEating[image.sprite.name];
  }

  void LoadSpritesheet()
  {
    var sprites = Resources.LoadAll<Sprite>("age" + Mathf.FloorToInt(age));
    var images = Resources.LoadAll<Sprite>("eat" + Mathf.FloorToInt(age));

    spritesheetMovement = sprites.ToDictionary(x => x.name, x => x);
    spritesheetEating = images.ToDictionary(x => x.name, x => x);
    spriteAge = Mathf.FloorToInt(age);
  }


  public void Die()
  {
    dead = true;
    anim.SetBool("Dead", true);
  }

  public void Eat()
  {
    // age = 0;
    if (!dead && !eating)
      StartCoroutine(Delay());
  }

  IEnumerator Delay() {
    eating = true;
    eatingAnimation.SetTrigger("Eat");
    yield return new WaitForSeconds(1.375f);
    age = 0;
    eating = false;
  }

  void OnCollisionEnter2D(Collision2D collisionInfo)
  {
    if (collisionInfo.collider.gameObject.layer == 3){
      Die();
    }
  }
}
