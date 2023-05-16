// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// -----------------------------------------------------------------------------------------
// player movement class
public class PlayerMovement : MonoBehaviour
{
    // static public members
    public static PlayerMovement instance;

    // -----------------------------------------------------------------------------------------
    // public members
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Transform playerMovePoint;
    public LayerMask whatStopsMovement;
    public LayerMask ice;
    public Animator animator;

    // -----------------------------------------------------------------------------------------
    // private members
    private Vector2 movement;
    private bool isCastingSpell = false;
    private bool isSliding = false;

    // -----------------------------------------------------------------------------------------
    // awake method to initialisation
    void Awake()
    {
        instance = this;

        if (!PlayerPrefs.HasKey("Top"))
        {
            PlayerPrefs.SetString("Top", "Z");
        }

        if (!PlayerPrefs.HasKey("Bottom"))
        {
            PlayerPrefs.SetString("Bottom", "S");
        }

        if (!PlayerPrefs.HasKey("Left"))
        {
            PlayerPrefs.SetString("Left", "Q");
        }

        if (!PlayerPrefs.HasKey("Right"))
        {
            PlayerPrefs.SetString("Right", "D");
        }
    }

    // -----------------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        playerMovePoint.parent = null;
    }

    // -----------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (!isCastingSpell)
        {
            PlayerOrientation();
            // if (Physics2D.OverlapCircle(this.transform.position, .2f, ice))
            // {
            //     MovePlayerOnIce();
            // }
            // else
            // {
            MovePlayer();
            // }
            PlayerIsMovingAnimation();
            PlayerSpellCast();
        }
    }

    // -----------------------------------------------------------------------------------------
    // move player method
    void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerMovePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, playerMovePoint.position) <= .05f)
        {
            isSliding = false;
            if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Top"))))
            {
                if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, 0.5f, 0f), .2f, whatStopsMovement))
                {
                    playerMovePoint.position += new Vector3(0f, 0.5f, 0f);
                    if (Physics2D.OverlapCircle(playerMovePoint.position, .2f, ice))
                    {
                        animator.SetInteger("orientation", 0);
                        while (Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, 0.5f, 0f), .2f, ice)
                        && !Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, 0.5f, 0f), .2f, whatStopsMovement))
                        {
                            playerMovePoint.position += new Vector3(0f, 0.5f, 0f);
                        }
                        if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, 0.5f, 0f), .2f, whatStopsMovement))
                            playerMovePoint.position += new Vector3(0f, 0.5f, 0f);
                    }
                }
                return;
            }
            else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Bottom"))))
            {
                if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, -0.5f, 0f), .2f, whatStopsMovement))
                {
                    playerMovePoint.position += new Vector3(0f, -0.5f, 0f);
                    if (Physics2D.OverlapCircle(playerMovePoint.position, .2f, ice))
                    {
                        animator.SetInteger("orientation", 4);
                        while (Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, -0.5f, 0f), .2f, ice)
                        && !Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, -0.5f, 0f), .2f, whatStopsMovement))
                        {
                            playerMovePoint.position += new Vector3(0f, -0.5f, 0f);
                        }
                        if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, -0.5f, 0f), .2f, whatStopsMovement))
                            playerMovePoint.position += new Vector3(0f, -0.5f, 0f);
                    }
                }
                return;
            }
            else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Left"))))
            {
                if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(-0.5f, 0f, 0f), .2f, whatStopsMovement))
                {
                    playerMovePoint.position += new Vector3(-0.5f, 0f, 0f);
                    if (Physics2D.OverlapCircle(playerMovePoint.position, .2f, ice))
                    {
                        animator.SetInteger("orientation", 2);
                        while (Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(-0.5f, 0f, 0f), .2f, ice)
                        && !Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(-0.5f, 0f, 0f), .2f, whatStopsMovement))
                        {
                            playerMovePoint.position += new Vector3(-0.5f, 0f, 0f);
                        }
                        if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(-0.5f, 0f, 0f), .2f, whatStopsMovement))
                            playerMovePoint.position += new Vector3(-0.5f, 0f, 0f);
                    }
                }
                return;
            }
            else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Right"))))
            {
                if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0.5f, 0f, 0f), .2f, whatStopsMovement))
                {
                    playerMovePoint.position += new Vector3(0.5f, 0f, 0f);
                    if (Physics2D.OverlapCircle(playerMovePoint.position, .2f, ice))
                    {
                        animator.SetInteger("orientation", 6);
                        while (Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0.5f, 0f, 0f), .2f, ice)
                        && !Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0.5f, 0f, 0f), .2f, whatStopsMovement))
                        {
                            playerMovePoint.position += new Vector3(0.5f, 0f, 0f);
                        }
                        if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0.5f, 0f, 0f), .2f, whatStopsMovement))
                            playerMovePoint.position += new Vector3(0.5f, 0f, 0f);
                    }
                }
                return;
            }
        }
        else
        {
            isSliding = true;
        }
    }

    // -----------------------------------------------------------------------------------------
    // player orientation method
    void PlayerOrientation()
    {
        if (Vector3.Distance(transform.position, playerMovePoint.position) > .05f
        && Physics2D.OverlapCircle(this.transform.position, .2f, ice))
        {
            return;
        }

        if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Top"))))
        {
            animator.SetInteger("orientation", 0);
            return;
        }
        else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Bottom"))))
        {
            animator.SetInteger("orientation", 4);
            return;
        }
        else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Left"))))
        {
            animator.SetInteger("orientation", 2);
            return;
        }
        else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Right"))))
        {
            animator.SetInteger("orientation", 6);
            return;
        }
    }

    // -----------------------------------------------------------------------------------------
    // player is moving animation method
    void PlayerIsMovingAnimation()
    {
        if (Physics2D.OverlapCircle(this.transform.position, .2f, ice))
        {
            animator.SetBool("isMoving", false);
            return;
        }

        // Gestion isMoving Animation
        if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Right")))
        || Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Left")))
        || Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Top")))
        || Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Bottom"))))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    // -----------------------------------------------------------------------------------------
    // player spellcast method
    void PlayerSpellCast()
    {
        if (isSliding)
            return;

        // TEMPORAIRE -- TESTS
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("spellCast1", true);
            isCastingSpell = true;
            StartCoroutine(SetAnimationWithTime("spellCast1", 0.517f));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("spellCast2", true);
            isCastingSpell = true;
            StartCoroutine(SetAnimationWithTime("spellCast2", 0.433f));
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetBool("death", true);
            isCastingSpell = true;
            StartCoroutine(SetAnimationWithTime("death", 4f));
        }
    }

    IEnumerator SetAnimationWithTime(string spellName, float spellTime)
    {
        yield return new WaitForSeconds(spellTime);
        animator.SetBool(spellName, false);
        isCastingSpell = false;
    }
}