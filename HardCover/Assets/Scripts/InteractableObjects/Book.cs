using UnityEngine;

public class Book : MonoBehaviour, IInteractableAndActionable
{
    private bool isInteracting, isActioning;

    //TODO: KEFF FILL IN WHAT A BOOK NEEDS
    private float mEnergyCost;

    public float energyCost
    {
        get => mEnergyCost;
        set => mEnergyCost = value;
    }

    private float mKnowledgeValue;

    public float knowledgeValue
    {
        get => mKnowledgeValue;
        set => mKnowledgeValue = value;
    }

    public void StartInteraction(GameObject interactor)
    {
        // Pick Up Book
        isInteracting = true;
        Debug.Log("Pick up Book");
    }

    public void EndInteraction()
    {
        // Drop Book
        isInteracting = false;
        Debug.Log("Drop Book");
    }

    public void StartAction()
    {
        // Start Reading
        isActioning = true;
        Debug.Log("Start Reading");
    }

    public void EndAction()
    {
        // Stop Reading
        isActioning = false;
        Debug.Log("Stop Reading");
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