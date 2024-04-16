using UnityEngine;

public class Interaction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.F; // Change to F for interaction
    public GameObject requiredItemPrefab; // Reference to the required item prefab
    public Inventory playerInventory; // Reference to the player's inventory

    public void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            if (playerInventory.HasItem(requiredItemPrefab))
            {
                PerformInteraction();
            }
            else
            {
                Debug.Log("You need the required item to interact with this object.");
            }
        }
    }

    void PerformInteraction()
    {
        // Implement your interaction logic here
        Debug.Log("Interaction performed!");

        // Destroy the MainGate object
        GameObject mainGate = GameObject.FindWithTag("MainGate"); // Assuming the gate has a tag "MainGate"
        if (mainGate != null)
        {
            Destroy(mainGate);
        }
    }
}
