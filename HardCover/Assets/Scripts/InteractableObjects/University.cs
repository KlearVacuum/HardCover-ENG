using UnityEngine;

public class University : MonoBehaviour, IInteractable
{
    public void StartInteraction(GameObject interactor)
    {
        if (GlobalGameData.playerStats.CanPassUni())
        {
            GlobalGameData.blackScreenOverlay.GoToScene("End_UniSuccess");
        }
    }

    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Default;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}