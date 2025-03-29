using System.Collections;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject continueButton;
    public TextMeshProUGUI dialogueText;
    public string[] preTaskDialogue;
    public string[] postTaskDialogue;
    private string[] currentDialogue;
    private int index = 0;
    public PlayerController playerController;
    public float wordSpeed = 0.05f;
    public bool playerIsClose;
    public bool taskCompleted = false;

    public GameObject npcProximityImage;

    void Start()
    {
        dialogueText.text = "";
        npcProximityImage.SetActive(false);
        currentDialogue = preTaskDialogue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                continueButton.SetActive(true);
                StartCoroutine(Typing());

                if (playerController != null)
                {
                    playerController.SetCanMove(false);
                }
            }
            else if (dialogueText.text == currentDialogue[index])
            {
                NextLine();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)
        {
            RemoveText();
        }
    }


    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);

        if (playerController != null)
        {
            playerController.SetCanMove(true);
        }
    }

    IEnumerator Typing()
    {
        dialogueText.text = "";
        foreach (char letter in currentDialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < currentDialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    public void MarkTaskAsCompleted()
    {
        taskCompleted = true;
        continueButton.SetActive(false);
        currentDialogue = postTaskDialogue;
        index = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            npcProximityImage.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            npcProximityImage.SetActive(false);
            RemoveText();
        }
    }
}