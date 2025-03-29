using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    public void ShowDialog(string message)
    {
        dialogBox.SetActive(true);
        dialogText.text = message;
    }

    public void HideDialog()
    {
        dialogBox.SetActive(false);
    }
}