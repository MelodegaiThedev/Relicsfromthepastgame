using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 10;

    // Detect collision with other GameObjects
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
    }
}
