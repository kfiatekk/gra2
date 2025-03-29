// 20.03.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using TMPro;
using UnityEngine;

public class TaskUpdate : MonoBehaviour
{
    public TaskList taskList;
    public TextMeshProUGUI taskText;

    private void Update()
    {
        if (taskList != null && taskText != null)
        {
            // Display only the current task
            int currentIndex = taskList.CurrentTaskIndex;
            if (currentIndex < taskList.Tasks.Count)
            {
                Task currentTask = taskList.Tasks[currentIndex];
                taskText.text = $"{currentTask.Name} - {(currentTask.IsCompleted ? "Ukoñczono" : "Nie ukoñczono")}";
            }
        }
    }
}