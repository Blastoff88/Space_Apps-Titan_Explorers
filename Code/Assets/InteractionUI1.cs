using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractableUI1 : MonoBehaviour
{
    public GameObject uiPanel; // Reference to the UI panel.
    public TMP_InputField inputField; // Reference to the TMP Input Field.
    public Button submitButton; // Reference to the submit button.

    public GameObject ui2; // Reference to the second UI panel.
    public Button ui2CloseButton; // Reference to the close button in ui2.

    private bool isInteracting = false; // Flag to track if the player is interacting.

    private void Start()
    {
        // Ensure the UI panels are initially hidden.
        uiPanel.SetActive(false);
        ui2.SetActive(false);
        submitButton.interactable = true;

        // Add an onClick listener to the close button in ui2.
        ui2CloseButton.onClick.AddListener(CloseUI2);
    }

    private void Update()
    {
        // Raycast to check for interactable objects.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Interactable1"))
            {
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

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            uiPanel.SetActive(false);
            isInteracting = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SubmitMessage()
    {
        // Handle the submit button click.
        string message = inputField.text;
        Debug.Log("Submit button clicked.");

        // Save the message to a text file on your computer.
        // Add your code to save the message here.

        // Hide the first UI after submission.
        uiPanel.SetActive(false);
        isInteracting = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Function to open the second UI panel (ui2).
    public void OpenUI2()
    {
        ui2.SetActive(true);
    }

    // Function to close the second UI panel (ui2).
    public void CloseUI2()
    {
        ui2.SetActive(false);
    }
}
