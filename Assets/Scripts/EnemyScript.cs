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
  // The name of the sprite sheet to use
  public string SpriteSheetName;
  string firstSpritesheetName;

  // The name of the currently loaded sprite sheet
  private string LoadedSpriteSheetName;

  // The dictionary containing all the sliced up sprites in the sprite sheet
  private Dictionary<string, Sprite> spriteSheet;

  // The Unity sprite renderer so that we don't have to get it multiple times
  private SpriteRenderer spriteRenderer;

  public float health = 3.0f;

  public Collider2D enemy_collider;

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    enemy_collider = GetComponent<Collider2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    firstSpritesheetName = SpriteSheetName;
    playerScript = player.GetComponent<PlayerMovement>();
    LoadSpriteSheet();
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
        Vector2 perlinVector = new Vector3(Mathf.PerlinNoise(Time.fixedTime * noiseScale, 0) * 2 - 1, Mathf.PerlinNoise(0, Time.fixedTime * noiseScale) * 2 - 1);
        if (perlinVector.sqrMagnitude > movementThreshold)
          movementVector = perlinVector;
      }

      rb.MovePosition(transform.position + Vector3.Normalize(movementVector) * Time.fixedDeltaTime * speed / 2 );
      Vector2 velocity = (transform.position - lastPos) / Time.fixedDeltaTime;

      anim.SetFloat("MoveX", velocity.x);
      anim.SetFloat("MoveY", velocity.y);
      anim.SetFloat("Speed", velocity.sqrMagnitude);
      lastPos = transform.position;
    }
    else {
      //Debug.Log("he shouldn't move after this :(");
      speed = 0f;
      rb.isKinematic = true;
      enemy_collider.enabled = false;

    }

    if (health <= 0.0) {
      // Debug.Log("Health is 0 or less. Enemy has died.");
      Die();
    }

  }

  public void Die()
  {
    dead = true;
    anim.SetBool("Dead", true);

  }

  // Runs after the animation has done its work
  private void LateUpdate()
  {
    // Check if the sprite sheet name has changed (possibly manually in the inspector)
    if (LoadedSpriteSheetName != SpriteSheetName)
    {
      // Load the new sprite sheet
      LoadSpriteSheet();
    }

    // Swap out the sprite to be rendered by its name
    // Important: The name of the sprite must be the same!
    int index = int.Parse(spriteRenderer.sprite.name.Split('_')[1]);
    spriteRenderer.sprite = spriteSheet[SpriteSheetName + "_" + index];
  }

  // Loads the sprites from a sprite sheet
  private void LoadSpriteSheet()
  {
    // Load the sprites from a sprite sheet file (png). 
    // Note: The file specified must exist in a folder named Resources
    var sprites = Resources.LoadAll<Sprite>(SpriteSheetName);
    spriteSheet = sprites.ToDictionary(x => x.name, x => x);

    // Remember the name of the sprite sheet in case it is changed later
    LoadedSpriteSheetName = SpriteSheetName;
  }

  void OnHit(float damage) {
    Debug.Log("Enemy was hit for " + damage);
    health = health - damage;
  }

}
