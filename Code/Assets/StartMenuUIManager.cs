using UnityEngine;
using UnityEngine.UI;

public class StartMenuUIManager : MonoBehaviour
{
    public GameObject uiMenu; // Reference to the UI menu you want to control.

    // Function to deactivate the UI menu.
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void DeactivateMenu()
    {
        uiMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
