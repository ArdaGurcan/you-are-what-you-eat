using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D rb;
  Animator anim;
  SpriteRenderer sr;
  [SerializeField]
  Image image;

  public Image dialogueBox;
  public Sprite[] dialogueImg;
  public Animator eatingAnimation;
  [SerializeField]
  float speed = 5f;

  public GameObject swordHitbox;
  Collider2D swordCollider;

  [SerializeField]
  float age = 0;
  [SerializeField]
  public bool dead = false;
  bool invincible = false;
  public bool eating = false;
  public static bool hasScapel = false;
  float extraSpeed = 1f;
  int spriteAge;


  public AudioSource audioData;
  public AudioClip deathSound;
  public AudioClip swingSound;
  public AudioClip eatSound;
  public AudioClip powerSound;
  public AudioClip hitFloor;
  public string priorityDialogue = "";
  public static Vector3 lastCheckpoint = Vector3.zero;
  Dictionary<string, Sprite> spritesheetMovement;
  Dictionary<string, Sprite> spritesheetEating;

  Vector3 inputVector;

  public string[] powerupDialogues;
  void Start()
  {
    if (lastCheckpoint.Equals(Vector3.zero))
      lastCheckpoint = new Vector3(-5.5f,-0.4f,0);
    transform.position = lastCheckpoint;
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    sr = GetComponent<SpriteRenderer>();
    audioData = GetComponent<AudioSource>();

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
    if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

  }


  void FixedUpdate()
  {
    if (!dead && !eating)
    {
      age += Time.fixedDeltaTime / 5f;
      rb.MovePosition(transform.position + Vector3.Normalize(inputVector) * Time.fixedDeltaTime * speed * extraSpeed);
    }

    if (dead)
    {
      rb.isKinematic = true;
    }
  }

  void LateUpdate()
  {
    if (!dead)
    {
      if (age >= 4f)
      {
        Die();

      }
      if (Mathf.FloorToInt(Mathf.Clamp(age, 0, 2.9f)) != spriteAge)
      {

        LoadSpritesheet();
      }
      speed = 5f - 0.75f * age;

    }
    sr.sprite = spritesheetMovement[sr.sprite.name];
    // Debug.Log(image.sprite.name);
    image.sprite = spritesheetEating[image.sprite.name];
  }

  void LoadSpritesheet()
  {
    var sprites = Resources.LoadAll<Sprite>("age" + Mathf.FloorToInt(Mathf.Clamp(age, 0, 2.9f)));
    var images = Resources.LoadAll<Sprite>("eat" + Mathf.FloorToInt(Mathf.Clamp(age, 0, 2.9f)));

    spritesheetMovement = sprites.ToDictionary(x => x.name, x => x);
    spritesheetEating = images.ToDictionary(x => x.name, x => x);
    spriteAge = Mathf.FloorToInt(Mathf.Clamp(age, 0, 2.9f));
    dialogueBox.sprite = dialogueImg[spriteAge];
  }


  public void Die()
  {
    if (!dead) {

    dead = true;
    audioData.clip = deathSound;
    audioData.Play();
    anim.SetBool("Dead", true);
    eatingAnimation.SetTrigger("Die");
    }
  }


  public void Eat(int type)
  {
    // age = 0;
    if (!dead && !eating)
    {
      if (type == 1) // speed
        StartCoroutine(Speed());
      else if (type == 2)
        StartCoroutine(Invincible());
      else if (type == 3)
        StartCoroutine(Slow());
      eating = true;
      StartCoroutine(Delay());
      StartCoroutine(SoundDelay(type));
    }
  }

  IEnumerator Invincible()
  {
    gameObject.layer = 6;
    priorityDialogue = powerupDialogues[1];
    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .6f);
    invincible = true;
    yield return new WaitForSeconds(5f);
    invincible = false;
    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
    priorityDialogue="";
    gameObject.layer = 0;

  }

  IEnumerator Speed()
  {
    priorityDialogue = powerupDialogues[0];
    sr.color = new Color((16 * 9 + 8) / 255f, 1, (16 * 9 + 4) / 255f, sr.color.a);
    extraSpeed *= 1.4f;
    yield return new WaitForSeconds(5f);
    extraSpeed /= 1.4f;
    sr.color = new Color(1, 1, 1, sr.color.a);
    priorityDialogue="";

  }

  IEnumerator Slow()
  {
    priorityDialogue = powerupDialogues[2];
    sr.color = new Color(1, 0.5613208f, 0.6534183f, sr.color.a);
    extraSpeed *= .6f;
    yield return new WaitForSeconds(5f);
    extraSpeed /= .6f;
    sr.color = new Color(1, 1, 1, sr.color.a);
    priorityDialogue="";

  }

  IEnumerator Delay()
  {

    eatingAnimation.SetTrigger("Eat");

    yield return new WaitForSeconds(1.375f);
    age = 0;
    eating = false;
  }

  IEnumerator SoundDelay(int type)
  {
    if(age >= 2)
      yield return new WaitForSeconds(0.2f);
    yield return new WaitForSeconds(0.375f);
    audioData.clip = eatSound;
    audioData.Play();
    yield return new WaitForSeconds(audioData.clip.length + 0.314f);

    if(type != 0) {
      audioData.clip = powerSound;
      audioData.Play();
      yield return new WaitForSeconds(audioData.clip.length);
    }
  }

  void OnCollisionEnter2D(Collision2D collisionInfo)
  {
    if (collisionInfo.collider.gameObject.layer == 3 && !invincible)
    {
      Die();
    }
  }
  void OnCollisionStay2D(Collision2D collisionInfo)
  {
    if (collisionInfo.collider.gameObject.layer == 3 && !invincible)
    {
      Die();
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if(other.CompareTag("Checkpoint")){
      lastCheckpoint = other.transform.position;
      
    }
  }
}
