using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class NPCInteraction : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string preTaskMessage = "Czy mozesz mi pomoc?";
    public string postTaskMessage = "Dziekuje za pomoc!";
    public string noHelpNeededMessage = "Nie potrzebuje juz pomocy.";

    public GameObject[] disappearingNPCs;
    public GameObject trashPrefab;
    public GameObject continueButton;
    public Transform[] spawnPoints;
    public GameObject npcProximityImage;
    public TaskList taskList;
    private Vector3[] initialPositions;
    private bool taskCompleted = false;
    private bool taskStarted = false;
    private bool playerIsClose;

    private int collectedTrash = 0;
    private int totalTrash;

    private void Start()
    {
        initialPositions = new Vector3[disappearingNPCs.Length];
        for (int i = 0; i < disappearingNPCs.Length; i++)
        {
            initialPositions[i] = disappearingNPCs[i].transform.position;
        }

        npcProximityImage?.SetActive(false);
        dialoguePanel.SetActive(false);
        totalTrash = spawnPoints.Length;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;

            if (!taskStarted && !taskCompleted)
            {
                npcProximityImage?.SetActive(true);
            }
            else if (taskCompleted)
            {
                dialoguePanel.SetActive(true);
                npcProximityImage.SetActive(false);
                dialogueText.text = "Nie ma ju¿ wiêcej œmieci do zbierania, dziêkujê za pomoc.";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            npcProximityImage?.SetActive(false);
            HideDialogue();
        }
    }

    // 19.03.2025 AI-Tag
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && taskCompleted == false && !taskStarted)
        {
            StartTask();
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerIsClose && taskCompleted)
        {
            ShowNoHelpNeededDialog();
        }
    }

    // 19.03.2025 AI-Tag
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    private void StartTask()
    {
        taskStarted = true;
        HideDialogue();
        foreach (GameObject npc in disappearingNPCs)
        {
            npc.SetActive(false);
        }

        foreach (Transform point in spawnPoints)
        {
            GameObject trash = Instantiate(trashPrefab, point.position, Quaternion.identity);
            TrashCollectible collectible = trash.GetComponent<TrashCollectible>();
            if (collectible != null)
            {
                collectible.npcInteraction = this;
            }
        }

        npcProximityImage?.SetActive(false);

        // Start the task in the task list
        if (taskList != null)
        {
            taskList.Tasks[0].OnStart.Invoke(); // Assuming the first task is to collect trash
        }
    }

    public void CollectTrash()
    {
        collectedTrash++;
        if (collectedTrash == totalTrash)
        {
            CompleteTask();
        }
    }

    // 19.03.2025 AI-Tag
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    private void CompleteTask()
    {
        taskCompleted = true;
        ResetNPCs();
        ShowPostTaskDialog();

        // Complete the task in the task list
        if (taskList != null)
        {
            taskList.Tasks[0].OnComplete.Invoke(); // Assuming the first task is to collect trash
        }
        if (continueButton != null)
        {
            continueButton.SetActive(false); // This will make the button disappear
        }
    }

    private void ResetNPCs()
    {
        for (int i = 0; i < disappearingNPCs.Length; i++)
        {
            disappearingNPCs[i].SetActive(true);
            disappearingNPCs[i].transform.position = initialPositions[i];
        }
    }

    private void ShowPreTaskDialog()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = preTaskMessage;
    }

    private void ShowPostTaskDialog()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = postTaskMessage;
        Invoke(nameof(HideDialogue), 5f);
    }

    private void ShowNoHelpNeededDialog()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = noHelpNeededMessage;
        Invoke(nameof(HideDialogue), 3f);
    }

    private void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
