// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// -----------------------------------------------------------------------------------------
// player movement class
public class PNJAppearance : MonoBehaviour
{
    // enum for the player orientation
    public enum Direction
    {
        Top = 0,
        Bottom = 4,
        Left = 2,
        Right = 6
    }

    // static public members
    public static PNJAppearance instance;

    // -----------------------------------------------------------------------------------------
    // public members
    public Transform tf;
    public Vector2 movement;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public string PnjSpriteSheetName;
    public List<string> PnjDialogue;
    public bool randomRotation;
    public bool isInteracting;
    public bool isDialogueRandom;
    public Direction startingDirection;
    

    // -----------------------------------------------------------------------------------------
    // private members
    private Vector2 previousPosition;

    // The name of the currently loaded sprite sheet
    private string LoadedSpriteSheetName;

    // The dictionary containing all the sliced up sprites in the sprite sheet
    private Dictionary<string, Sprite> spriteSheet;

    // -----------------------------------------------------------------------------------------
    // awake method to initialisation
    void Awake()
    {
        instance = this;
        previousPosition = tf.position;
        //velocity = rb.velocity;
        this.LoadSpriteSheet();
        SetStartingDirection();
    }

    // -----------------------------------------------------------------------------------------
    // update methode
    void Update()
    {
        if (PnjSpriteSheetName == PlayerPrefs.GetString("CharacterSelected"))
        {
            this.spriteRenderer.enabled = false;
            this.boxCollider.enabled = false;
        }
        else
        {
            this.spriteRenderer.enabled = true;
            this.boxCollider.enabled = true;
        }

        if (randomRotation && !isInteracting)
        {
            StartCoroutine(RotatePnjWithCooldown());
        }
    }

    // -----------------------------------------------------------------------------------------
    // fixed update methode
    void FixedUpdate()
    {
        movement.x = tf.position.x - previousPosition.x;
        movement.y = tf.position.y - previousPosition.y;

        previousPosition = tf.position;
    }

        // Runs after the animation has done its work
        private void LateUpdate()
    {
        // Check if the sprite sheet name has changed (possibly manually in the inspector)
        if (this.LoadedSpriteSheetName != PnjSpriteSheetName)
        {
            // Load the new sprite sheet
            this.LoadSpriteSheet();
        }

        // Swap out the sprite to be rendered by its name
        // Important: The name of the sprite must be the same!
        this.spriteRenderer.sprite = this.spriteSheet[this.spriteRenderer.sprite.name];
    }

    // -----------------------------------------------------------------------------------------
    // Loads the sprites from a sprite sheet
    private void LoadSpriteSheet()
    {
        // Load the sprites from a sprite sheet file (png). 
        // Note: The file specified must exist in a folder named Resources
        string spritesheetfolder = "Characters/";
        string spritesheetfilepath = spritesheetfolder + PnjSpriteSheetName + "/spritesheet";
        var sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
        if (sprites.Count() == 0)
        {
            spritesheetfilepath = spritesheetfolder + "Garde01/spritesheet";
            sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
        }

        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);

        // Remember the name of the sprite sheet in case it is changed later
        this.LoadedSpriteSheetName = PlayerPrefs.GetString("CharacterSelected");
    }

    // -----------------------------------------------------------------------------------------
    // Rotate the pnj randomly
    IEnumerator RotatePnjWithCooldown()
    {
        randomRotation = false;
        int randomOrientation = Random.Range(0, 4) * 2;
        animator.SetInteger("orientation", randomOrientation);
        yield return new WaitForSeconds(Random.Range(1, 8));
        randomRotation = true;
    }

    // -----------------------------------------------------------------------------------------
    // Set the starting direction of the pnj
    public void SetStartingDirection()
    {
        switch (startingDirection)
        {
            case Direction.Top:
                animator.SetInteger("orientation", 0);
                break;
            case Direction.Bottom:
                animator.SetInteger("orientation", 4);
                break;
            case Direction.Left:
                animator.SetInteger("orientation", 2);
                break;
            case Direction.Right:
                animator.SetInteger("orientation", 6);
                break;
            default:
                animator.SetInteger("orientation", 4);
                break;
        }
    }
}
