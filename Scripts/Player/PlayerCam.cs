using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;
    float xRotation;
    float yRotation;

    [SerializeField]
    private GameObject dialogueMenu; // Reference to the DialogueMenu GameObject

    [SerializeField]
    private Transform target; // Reference to the target GameObject

    private Quaternion originalRotation; // Store the original rotation before DialogueMenu becomes active
    private Quaternion storedRotation; // Store the rotation when DialogueMenu is active

    private bool wasDialogueMenuActive = false; // Track if DialogueMenu was active in the previous frame

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Store the original rotation
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        bool isDialogueMenuActive = dialogueMenu != null && dialogueMenu.activeSelf;

        if (isDialogueMenuActive && !wasDialogueMenuActive) // If DialogueMenu becomes active
        {
            // Store the current rotation
            storedRotation = transform.rotation;
        }

        if (isDialogueMenuActive) // Check if the DialogueMenu is active
        {
            // Set rotation to stored rotation if DialogueMenu is active
            transform.rotation = storedRotation;
            orientation.rotation = Quaternion.Euler(0, storedRotation.eulerAngles.y, 0);

            if (target != null)
            {
                // Rotate the camera towards the target
                Vector3 direction = target.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, 0);
                orientation.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }
        else
        {
            // Rotate the camera normally
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        wasDialogueMenuActive = isDialogueMenuActive;
    }
}
