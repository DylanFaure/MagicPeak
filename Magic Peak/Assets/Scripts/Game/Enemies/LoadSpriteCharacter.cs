using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LoadSpriteCharacter : MonoBehaviour
{
    public static LoadSpriteCharacter instance;

    [SerializeField] private string SpriteSheetName;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 previousPosition;
    private Vector2 movement;
    private string LoadedSpriteSheetName;
    private Dictionary<string, Sprite> spriteSheetDictionnary;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        instance = this;
        previousPosition = transform.position;
        this.LoadSpriteSheet();
        animator.SetFloat("speed", 0);
        animator.SetInteger("orientation", 4);
    }

    void FixedUpdate()
    {
        movement.x = transform.position.x - previousPosition.x;
        movement.y = transform.position.y - previousPosition.y;

        previousPosition = transform.position;

        AnimationUpdate();
    }

    private void LateUpdate()
    {
        if (this.LoadedSpriteSheetName != this.SpriteSheetName)
        {
            this.LoadSpriteSheet();
        }

        this.spriteRenderer.sprite = this.spriteSheetDictionnary[this.spriteRenderer.sprite.name];
    }

    public void AnimationUpdate()
    {
        animator.SetFloat("speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        if (movement.x > 0)
            animator.SetInteger("orientation", 6);
        if (movement.x < 0)
            animator.SetInteger("orientation", 2);
        if (movement.y > 0)
            animator.SetInteger("orientation", 0);
        if (movement.y < 0)
            animator.SetInteger("orientation", 4);
    }

    private void LoadSpriteSheet()
    {
        string spritesheetfolder = "Enemies/";
        string spritesheetfilepath = spritesheetfolder + this.SpriteSheetName + "/spritesheet";
        var sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
        if (sprites.Count() == 0)
        {
            spritesheetfilepath = spritesheetfolder + "Jin/spritesheet";
            sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
        }

        this.spriteSheetDictionnary = sprites.ToDictionary(x => x.name, x => x);
        this.LoadedSpriteSheetName = this.SpriteSheetName;
    }
}
