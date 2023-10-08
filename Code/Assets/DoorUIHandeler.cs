using UnityEngine;
using UnityEngine.UI;

public class DoorUIHandeler : MonoBehaviour
{
    public GameObject uiPanel; // Reference to the UI panel.
    public GameObject objectToDisappear; // Reference to the 3D object to disappear.
    public Button closeButton; // Reference to the UI button that closes the UI.

    private bool isInteracting = false; // Flag to track if player is interacting.

    private void Start()
    {
        // Ensure the UI panel is initially hidden.
        uiPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseUIAndDisappearObject);
    }

    private void Update()
    {
        // Raycast to check for objects tagged as "Interactable2."
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Interactable2"))
            {
                // Show the interaction text and listen for input.
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isInteracting = true;
                    uiPanel.SetActive(true);
                }
            }
        }

        if (isInteracting)
        {
            // Allow text input while the cursor is visible.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void CloseUIAndDisappearObject()
    {
        // Handle the close button click.
        if (objectToDisappear != null)
        {
            objectToDisappear.SetActive(false); // Make the 3D object disappear.
        }

        uiPanel.SetActive(false); // Close the UI pop-up.
        isInteracting = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
