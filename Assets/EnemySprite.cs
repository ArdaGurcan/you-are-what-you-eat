using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemySprite : MonoBehaviour
{
  // The name of the sprite sheet to use
  public string SpriteSheetName;
  string firstSpritesheetName;

  // The name of the currently loaded sprite sheet
  private string LoadedSpriteSheetName;

  // The dictionary containing all the sliced up sprites in the sprite sheet
  private Dictionary<string, Sprite> spriteSheet;

  // The Unity sprite renderer so that we don't have to get it multiple times
  private SpriteRenderer spriteRenderer;

  static int spriteIndex = 0;
  public int currentSpriteIndex;
  // Start is called before the first frame update
  void Start()
  {
    currentSpriteIndex = (spriteIndex++) % 6 + 1;
    spriteRenderer = GetComponent<SpriteRenderer>();
    firstSpritesheetName = "enemy" + currentSpriteIndex;
    LoadSpriteSheet();
  }

  // Runs after the animation has done its work
  private void LateUpdate()
  {
    // Check if the sprite sheet name has changed (possibly manually in the inspector)
    if (LoadedSpriteSheetName != "enemy" + currentSpriteIndex)
    {
      // Load the new sprite sheet
      LoadSpriteSheet();
    }

    // Swap out the sprite to be rendered by its name
    // Important: The name of the sprite must be the same!
    int index = int.Parse(spriteRenderer.sprite.name.Split('_')[1]);
    spriteRenderer.sprite = spriteSheet["enemy" + currentSpriteIndex + "_" + index];
  }

  // Loads the sprites from a sprite sheet
  private void LoadSpriteSheet()
  {
    // Load the sprites from a sprite sheet file (png). 
    // Note: The file specified must exist in a folder named Resources
    var sprites = Resources.LoadAll<Sprite>("enemy" + currentSpriteIndex);
    spriteSheet = sprites.ToDictionary(x => x.name, x => x);

    // Remember the name of the sprite sheet in case it is changed later
    LoadedSpriteSheetName = "enemy" + currentSpriteIndex;
  }
}
