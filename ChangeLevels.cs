using UnityEngine;
using UnityEngine.UI; // Jeœli u¿ywasz UI

public class Stairs : MonoBehaviour
{
    public Transform teleportTarget; // Miejsce, gdzie ma siê przenieœæ gracz
    public GameObject interactionImage; // Obrazek pojawiaj¹cy siê przy schodach
    private bool playerIsNear = false;
    private GameObject player;

    void Start()
    {
        if (interactionImage != null)
            interactionImage.SetActive(false); // Ukryj obrazek na starcie
    }

    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            player = other.gameObject;
            if (interactionImage != null)
                interactionImage.SetActive(true); // Poka¿ obrazek
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            if (interactionImage != null)
                interactionImage.SetActive(false); // Ukryj obrazek
        }
    }

    void TeleportPlayer()
    {
        if (player != null && teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;
        }
    }
}