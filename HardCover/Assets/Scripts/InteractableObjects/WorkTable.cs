using UnityEngine;

public class WorkTable : MonoBehaviour, IInteractableAndActionable
{
    private bool isInteracting, isActioning;

    //TODO: KEFF FILL IN WHAT A WORK NEEDS
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

    public void StartInteraction(GameObject interactor)
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