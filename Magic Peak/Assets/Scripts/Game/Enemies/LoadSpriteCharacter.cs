using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LoadSpriteCharacter : MonoBehaviour
{
    public static LoadSpriteCharacter instance;

    [SerializeField] private string SpriteSheetName;

    private SpriteRenderer spriteRenderer;
    private Vector2 previousPosition;
    private Vector2 movement;
    private string LoadedSpriteSheetName;
    private Dictionary<string, Sprite> spriteSheetDictionnary;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        instance = this;
        previousPosition = transform.position;
        this.LoadSpriteSheet();
    }

    void FixedUpdate()
    {
        movement.x = transform.position.x - previousPosition.x;
        movement.y = transform.position.y - previousPosition.y;

        previousPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (this.LoadedSpriteSheetName != this.SpriteSheetName)
        {
            this.LoadSpriteSheet();
        }

        this.spriteRenderer.sprite = this.spriteSheetDictionnary[this.spriteRenderer.sprite.name];
    }

    private void LoadSpriteSheet()
    {
        string spritesheetfolder = "Enemies/";
        string spritesheetfilepath = spritesheetfolder + this.SpriteSheetName + "/spritesheet";
        var sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
        if (sprites.Count() == 0)
        {
            spritesheetfilepath = spritesheetfolder + "guard-1/spritesheet";
            sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
        }

        this.spriteSheetDictionnary = sprites.ToDictionary(x => x.name, x => x);
        this.LoadedSpriteSheetName = this.SpriteSheetName;
    }
}
