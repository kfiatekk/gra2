// 19.03.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour

{
    public TextMeshProUGUI taskDisplayText;
    public List<Task> Tasks { get; private set; } // Make this property accessible
    public int CurrentTaskIndex => currentTaskIndex;
    private int currentTaskIndex;

    private void Start()
    {
        // Initialize tasks
        Tasks = new List<Task>
        {
            new Task("Pozbieraj Œmieci", StartCollectTrashTask, CompleteCollectTrashTask),
            new Task("Porozmawiaj z Panem Gacem", StartTalkToNPCTask, CompleteTalkToNPCTask)
        };
        currentTaskIndex = 0;

        if (Tasks.Count > 0)
        {
            // Start the first task
            Tasks[currentTaskIndex].OnStart.Invoke();
            UpdateTaskDisplay();
        }
    }

    private void Update()
    {
        if (currentTaskIndex < Tasks.Count)
        {
            Task currentTask = Tasks[currentTaskIndex];
            if (currentTask.IsCompleted)
            {
                currentTask.OnComplete.Invoke();
                currentTaskIndex++;
                if (currentTaskIndex < Tasks.Count)
                {
                    Tasks[currentTaskIndex].OnStart.Invoke();
                    UpdateTaskDisplay();
                }
            }
        }
    }

    private void StartCollectTrashTask()
    {
        Debug.Log("Starting Task: Collect Trash");
        // Logic for starting the task
    }

    private void CompleteCollectTrashTask()
    {
        Debug.Log("Completing Task: Collect Trash");
        Tasks[currentTaskIndex].IsCompleted = true;
        // Logic for completing the task
    }

    private void StartTalkToNPCTask()
    {
        Debug.Log("Starting Task: Talk to NPC");
        // Logic for starting the task
    }

    private void CompleteTalkToNPCTask()
    {
        Debug.Log("Completing Task: Talk to NPC");
        Tasks[currentTaskIndex].IsCompleted = true;
        // Logic for completing the task
    }
    private void UpdateTaskDisplay()
    {
        if (taskDisplayText != null)
        {
            taskDisplayText.text = Tasks[currentTaskIndex].Name;
        }
    }
}


public class Task
{
    public string Name { get; }
    public bool IsCompleted { get; set; }
    public System.Action OnStart { get; }
    public System.Action OnComplete { get; }

    public Task(string name, System.Action onStart, System.Action onComplete)
    {
        Name = name;
        IsCompleted = false;
        OnStart = onStart;
        OnComplete = onComplete;
    }
}