using System.Linq; // Dodajemy tê dyrektywê, aby móc u¿ywaæ metody Count
using UnityEngine;

public class PizzaDelivery : MonoBehaviour
{
    public GameObject interactIcon; // Ikona "E"
    private ShopTrigger shop;
    private bool playerInDeliveryPoint = false;
    private DialogManager dialogManager;

    private void Start()
    {
        shop = FindObjectOfType<ShopTrigger>();
        dialogManager = FindObjectOfType<DialogManager>();

        if (shop == null)
        {
            Debug.LogError("Nie znaleziono ShopTrigger!");
        }
        else
        {
            Debug.Log("ShopTrigger znaleziony: " + shop.name);
        }

        if (dialogManager == null)
        {
            Debug.LogError("DialogManager nie jest znaleziony w scenie!");
        }
        else
        {
            Debug.Log("DialogManager znaleziony: " + dialogManager.name);
        }

        if (interactIcon == null)
        {
            Debug.LogError("InteractIcon nie jest przypisany w Inspectorze!");
        }
        else
        {
            Debug.Log("InteractIcon przypisany: " + interactIcon.name);
        }

        interactIcon.SetActive(false); // Ukryj ikonê "E" na pocz¹tku
    }

    private void Update()
    {
        if (playerInDeliveryPoint && Input.GetKeyDown(KeyCode.E) && shop.IsMissionStarted())
        {
            Debug.Log("Gracz nacisn¹³ klawisz E w punkcie dostawy");
            CheckDelivery();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D wywo³ane z: " + other.name);
        if (other.CompareTag("Player") && shop.IsMissionStarted())
        {
            playerInDeliveryPoint = true;
            if (interactIcon != null)
            {
                interactIcon.SetActive(true); // Poka¿ ikonê "E"
            }
            Debug.Log("Gracz wszed³ do punktu dostawy: " + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D wywo³ane z: " + other.name);
        if (other.CompareTag("Player"))
        {
            playerInDeliveryPoint = false;
            if (interactIcon != null)
            {
                interactIcon.SetActive(false); // Ukryj ikonê "E"
            }
            Debug.Log("Gracz opuœci³ punkt dostawy: " + gameObject.name);
        }
    }

    private void CheckDelivery()
    {
        Debug.Log("CheckDelivery wywo³ane");
        if (shop == null)
        {
            Debug.LogError("Nie znaleziono ShopTrigger!");
            return;
        }

        Transform[] deliveryTargets = shop.GetDeliveryPoints();
        bool[] deliveriesCompleted = shop.GetDeliveriesCompleted();

        for (int i = 0; i < deliveryTargets.Length; i++)
        {
            if (deliveryTargets[i] == this.transform && !deliveriesCompleted[i])
            {
                Debug.Log("Pizza dostarczona poprawnie do: " + gameObject.name);
                shop.DeliverPizza(this.transform);
                return;
            }
        }

        int remainingDeliveries = shop.GetDeliveryPoints().Length - deliveriesCompleted.Count(x => x);
        string remainingMessage = "Zosta³o jeszcze " + remainingDeliveries + " miejsce/miejsca!";
        Debug.Log(remainingMessage);
        dialogManager.ShowDialog(remainingMessage);
    }
}