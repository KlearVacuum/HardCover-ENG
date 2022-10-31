using UnityEngine;

public class Bed : MonoBehaviour, IInteractableAndActionable
{
    private void ShowUi()
    {
    }

    private void HideUi()
    {
    }

    // ========================================================
    // ALL INTERFACE THINGS
    // ========================================================

    // ========================================================
    // IShowUiPopUp
    // ========================================================
    public void TriggerEnter(Collider2D collision)
    {
        Debug.Log("Show Ui");
    }

    public void TriggerExit(Collider2D collision)
    {
        Debug.Log("Hide Ui");
    }

    // ========================================================
    // IInteractableAndActionable
    // ========================================================
    private bool isInteracting, isActioning;

    public void StartInteraction(GameObject interactor)
    {
        // Pick Up Book
        isInteracting = true;
        Debug.Log("Enter Bed");
    }

    public void EndInteraction()
    {
        // Drop Book
        isInteracting = false;
        Debug.Log("Exit Bed");
    }

    public void StartAction()
    {
        // Start Reading
        isActioning = true;
        Debug.Log("Start Sleeping");
    }

    public void EndAction()
    {
        // Stop Reading
        isActioning = false;
        Debug.Log("Stop Sleeping");
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }

    public bool IsActioning()
    {
        return isActioning;
    }
}