using UnityEngine;

public class University : MonoBehaviour, IInteractable
{
    public void StartInteraction(GameObject interactor)
    {
        //Check if interactor is player
        //Check interactor stats
        if (true)
        {
            Debug.Log("Game Ends");
        }
    }
    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Default;
    }
}