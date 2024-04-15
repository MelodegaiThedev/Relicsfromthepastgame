using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Add this line to use UI components

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Text healthText; // Reference to the UI Text element

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText(); // Update the health text when the game starts
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0
        UpdateHealthText(); // Update the health text when the player takes damage

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Implement player death logic here
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString(); // Update the Text component with the current health value
        }
    }
}
