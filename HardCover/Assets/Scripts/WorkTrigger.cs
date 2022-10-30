using UnityEngine;

public class WorkTrigger : MonoBehaviour, IInteractableAndActionable
{
    private bool isInteracting, isActioning;

    private GameObject inCollisionWith;

    //TODO: KEFF FILL IN WHAT A BOOK NEEDS
    private float mEnergyCost;

    public float energyCost
    {
        get => mEnergyCost;
        set => mEnergyCost = value;
    }

    private float mCashValue;

    public float knowledgeValue
    {
        get => mCashValue;
        set => mCashValue = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inCollisionWith = collision.gameObject;
            collision.GetComponent<InteractionController>().interactableObj = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inCollisionWith = null;
            collision.GetComponent<InteractionController>().interactableObj = null;
        }
    }

    public void StartInteraction()
    {
        // Sit at workbench
        isInteracting = true;
        Debug.Log("Sit at workbench");
    }

    public void EndInteraction()
    {
        // Stand up from workbench
        isInteracting = false;
        Debug.Log("Stand up from workbench");
    }

    public void StartAction()
    {
        // Start working
        isActioning = true;
        Debug.Log("Start working");
    }

    public void EndAction()
    {
        // Stop Working
        isActioning = false;
        Debug.Log("Stop working");
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