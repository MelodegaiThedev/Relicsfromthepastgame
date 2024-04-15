using UnityEngine;

public class Ball : MonoBehaviour
{
    public int damageAmount = 10;
    public float destroyDelay = 1f; // Delay before destroying the ball

    // Reference to the enemy collider
    public Collider enemyCollider;

    // Detect collision with other GameObjects
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }

        // Ignore collision with the enemy collider
        if (enemyCollider != null)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), enemyCollider);
        }

        // Destroy the ball after a delay
        Destroy(gameObject, destroyDelay);
    }
}
