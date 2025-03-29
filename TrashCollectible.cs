using UnityEngine;

public class TrashCollectible : MonoBehaviour
{
    public NPCInteraction npcInteraction; // Referencja do skryptu NPCInteraction
    private bool isCollected = false; // Flaga, aby zapobiec wielokrotnemu zbieraniu

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected) return; // Jeœli ju¿ zebrano, wyjdŸ z metody
        if (collision.CompareTag("Player"))
        {
            isCollected = true; // Oznacz œmieæ jako zebrany
            Debug.Log($"[TrashCollectible] Gracz zebra³ œmieæ: {gameObject.name}");

            Destroy(gameObject); // Usuñ obiekt œmieci
            if (npcInteraction != null)
            {
                npcInteraction.CollectTrash(); // Powiadom NPC o zebraniu œmieci
            }
            else
            {
                Debug.LogError("[TrashCollectible] Brak przypisania NPCInteraction!");
            }
        }
    }
}
