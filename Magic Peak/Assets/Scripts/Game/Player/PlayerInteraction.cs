using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    // -----------------------------------------------------------------------------------------
    // public members
    public Transform playerInteractionPoint;
    public Animator animator;
    public LayerMask interactionLayer;
    public GameObject interactionCanvas;
    public GameObject characterSelectionCanvas;

    void Awake()
    {
        interactionCanvas.SetActive(false);
        characterSelectionCanvas.SetActive(false);
    }

    // -----------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (animator.GetInteger("orientation") == 0)
            playerInteractionPoint.position = this.transform.position + new Vector3(0, 0.5f, 0);
        else if (animator.GetInteger("orientation") == 4)
            playerInteractionPoint.position = this.transform.position - new Vector3(0, 0.5f, 0);
        else if (animator.GetInteger("orientation") == 2)
            playerInteractionPoint.position = this.transform.position - new Vector3(0.5f, 0, 0);
        else if (animator.GetInteger("orientation") == 6)
            playerInteractionPoint.position = this.transform.position + new Vector3(0.5f, 0, 0);

        CheckInteraction();
    }

    // -----------------------------------------------------------------------------------------
    // Check if the player is interacting with a layer
    void CheckInteraction()
    {
        if (Physics2D.OverlapCircle(playerInteractionPoint.position, .2f, interactionLayer))
        {
            interactionCanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        else
        {
            interactionCanvas.SetActive(false);
        }
    }

    // -----------------------------------------------------------------------------------------
    // Interact with the object the player is interacting with
    void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerInteractionPoint.position, Vector2.zero, 0f, interactionLayer);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Pull"))
            {
                SceneManager.LoadScene("Pull");
            }
            else if (hit.collider.CompareTag("CharacterSelection"))
            {
                characterSelectionCanvas.SetActive(true);
            }
            else if (hit.collider.CompareTag("PNJ"))
            {
            }
        }
    }
}
