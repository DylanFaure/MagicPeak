using System.Net.NetworkInformation;
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
    public Animator animator;

    // -----------------------------------------------------------------------------------------
    // private members
    private Vector2 movement;

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
        MovePlayer();
    }

    // -----------------------------------------------------------------------------------------
    // move player method
    void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerMovePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, playerMovePoint.position) <= .05f)
        {
            if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Top"))))
            {
                if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, 1f, 0f), .2f, whatStopsMovement))
                {
                    playerMovePoint.position += new Vector3(0f, 1f, 0f);
                }
                return;
            }
            else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Bottom"))))
            {
                if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(0f, -1f, 0f), .2f, whatStopsMovement))
                {
                    playerMovePoint.position += new Vector3(0f, -1f, 0f);
                }
                return;
            }
            else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Left"))))
            {
                if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(-1f, 0f, 0f), .2f, whatStopsMovement))
                {
                    playerMovePoint.position += new Vector3(-1f, 0f, 0f);
                }
                return;
            }
            else if (Input.GetKey((KeyCode)System.Enum.Parse( typeof(KeyCode), PlayerPrefs.GetString("Right"))))
            {
                if (!Physics2D.OverlapCircle(playerMovePoint.position + new Vector3(1f, 0f, 0f), .2f, whatStopsMovement))
                {
                    playerMovePoint.position += new Vector3(1f, 0f, 0f);
                }
                return;
            }
        }
    }
}
