using UnityEngine;

public class Interaction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.E;
    public Inventory playerInventory;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            if (playerInventory.HasItemWithTag("KeyItem"))
            {
                PerformInteraction();
            }
            else
            {
                Debug.Log("You need the KeyItem to interact with this object.");
            }
        }
    }

    void PerformInteraction()
    {
        GameObject mainGate = GameObject.FindWithTag("MainGate");
        if (mainGate != null)
        {
            Destroy(mainGate);
            Debug.Log("Interaction performed!");
        }
    }
}
