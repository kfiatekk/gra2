using UnityEngine;

public class Pizza : MonoBehaviour
{
    private bool isCarried = false; // Czy gracz ma pizzê?

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCarried)
        {
            Debug.Log("Gracz podniós³ pizzê!");
            isCarried = true;
            gameObject.SetActive(false); // Ukryj pizzê po podniesieniu
        }
    }

    public bool IsCarried()
    {
        return isCarried;
    }

    public void DropPizza()
    {
        if (isCarried)
        {
            Debug.Log("Pizza zosta³a upuszczona!");
            isCarried = false;
            gameObject.SetActive(true); // Poka¿ pizzê po upuszczeniu
        }
    }
}