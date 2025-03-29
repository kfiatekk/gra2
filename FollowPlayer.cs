// 04.03.2025 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Assign the Player's Transform in the Inspector
    public Vector3 offset; // Adjust the offset in the Inspector to position the image correctly

    void Update()
    {
        if (playerTransform != null)
        {
            // Update the position of the Proximity image to follow the player
            transform.position = playerTransform.position + offset;
        }
    }
}