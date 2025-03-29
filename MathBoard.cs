using UnityEngine;
using UnityEngine.UI;

public class MathBoard : MonoBehaviour
{
    public GameObject mathPanel; // Panel z zadaniem matematycznym
    public Text questionText;
    public InputField answerInput;
    public Text feedbackText;

    private int correctAnswer;
    private bool isPlayerNear = false;

    void Start()
    {
        mathPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ShowMathProblem();
        }
    }

    void ShowMathProblem()
    {
        mathPanel.SetActive(true);
        int a = Random.Range(1, 10);
        int b = Random.Range(1, 10);
        correctAnswer = a + b;
        questionText.text = $"Ile to {a} + {b}?";
        feedbackText.text = "";
    }

    public void CheckAnswer()
    {
        if (int.TryParse(answerInput.text, out int playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                feedbackText.text = "Dobrze!";
                mathPanel.SetActive(false); // Zamknij po poprawnej odpowiedzi
            }
            else
            {
                feedbackText.text = "èle, sprÛbuj ponownie.";
            }
        }
        else
        {
            feedbackText.text = "Podaj liczbÍ!";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            mathPanel.SetActive(false);
        }
    }
}
