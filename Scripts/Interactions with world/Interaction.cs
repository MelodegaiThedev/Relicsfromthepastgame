using UnityEngine;

public class Interaction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.F;
    public Inventory playerInventory;
    public GameObject objectToDestroy;
    public GameObject requiredItemPrefab;

    public GameObject player;

    float distanceInteraction;

    void Update()
    {
    distanceInteraction = Vector3.Distance(player.transform.position, this.transform.position);

    // Check if the player is within interaction range and presses the interaction key
    if (distanceInteraction <= 3.5f && Input.GetKeyDown(interactionKey))
        {
        if (playerInventory.HasItemWithTag("KeyItem"))
        {
            PerformInteraction();
        }
        else
        {
            // Handle the case where the player doesn't have the required item
        }
        }
    }

    void PerformInteraction()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
            Debug.Log("Interaction performed!");
        }
    }
}
