using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    // -----------------------------------------------------------------------------------------
    // public members
    public Transform playerInteractionPoint;
    public Animator animator;
    public LayerMask interactionLayer;
    public GameObject interactionCanvas;
    public GameObject characterSelectionCanvas;
    public GameObject pnjDialogueCanvas;
    public TextMeshProUGUI pnjDialogueText;

    // -----------------------------------------------------------------------------------------
    // private members
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private List<string> pnjDialogueList;
    [SerializeField] private string characterDescription;
    [SerializeField] private TextMeshProUGUI characterDescriptionText;
    private bool isInteracting;
    private int dialogueIndex;
    private bool isDialogueRandomize;

    void Awake()
    {
        interactionCanvas.SetActive(false);
        characterSelectionCanvas.SetActive(false);
        playerMovement = GetComponent<PlayerMovement>();
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

        if (!isInteracting)
        {
            CheckInteraction();
        }
        else
        {
            Interact();
        }
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
                isInteracting = true;
            }
        }
        else
        {
            interactionCanvas.SetActive(false);
        }
    }

    // -----------------------------------------------------------------------------------------
    // Set the isInteracting value
    public void StopInteracting()
    {
        isInteracting = false;
        playerMovement.isTalkingToPnj = false;
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
                playerMovement.isTalkingToPnj = true;
            }
            else if (hit.collider.CompareTag("PNJ"))
            {
                if (playerMovement.isTalkingToPnj == false)
                    pnjDialogueList = hit.collider.GetComponent<PNJAppearance>().PnjDialogue;
                hit.collider.GetComponent<PNJAppearance>().isInteracting = true;
                if (animator.GetInteger("orientation") == 0)
                    hit.collider.GetComponent<PNJAppearance>().animator.SetInteger("orientation", 4);
                else if (animator.GetInteger("orientation") == 4)
                    hit.collider.GetComponent<PNJAppearance>().animator.SetInteger("orientation", 0);
                else if (animator.GetInteger("orientation") == 2)
                    hit.collider.GetComponent<PNJAppearance>().animator.SetInteger("orientation", 6);
                else if (animator.GetInteger("orientation") == 6)
                    hit.collider.GetComponent<PNJAppearance>().animator.SetInteger("orientation", 2);
                playerMovement.isTalkingToPnj = true;
                if (hit.collider.GetComponent<PNJAppearance>().isDialogueRandom && !isDialogueRandomize) {
                    dialogueIndex = Random.Range(0, pnjDialogueList.Count);
                    isDialogueRandomize = true;
                }

                pnjDialogueText.text = pnjDialogueList[dialogueIndex];
                pnjDialogueCanvas.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    if (hit.collider.GetComponent<PNJAppearance>().isDialogueRandom)
                    {
                        hit.collider.GetComponent<PNJAppearance>().isInteracting = false;
                        hit.collider.GetComponent<PNJAppearance>().SetStartingDirection();
                        pnjDialogueCanvas.SetActive(false);
                        playerMovement.isTalkingToPnj = false;
                        isInteracting = false;
                        dialogueIndex = 0;
                        isDialogueRandomize = false;
                    }
                    dialogueIndex++;
                    if (pnjDialogueList.Count == dialogueIndex)
                    {
                        hit.collider.GetComponent<PNJAppearance>().isInteracting = false;
                        hit.collider.GetComponent<PNJAppearance>().SetStartingDirection();
                        pnjDialogueCanvas.SetActive(false);
                        playerMovement.isTalkingToPnj = false;
                        isInteracting = false;
                        dialogueIndex = 0;
                        isDialogueRandomize = false;
                    }
                    else
                    {
                        pnjDialogueText.text = pnjDialogueList[dialogueIndex];
                    }
                }
            }
        }
    }
}
