using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{
    public Portal destination;

    public void Teleport(GameObject go)
    {
        if (GlobalGameData.portalCooldown <= 0 && go != null)
        {
            GlobalGameData.portalCooldown = 0.5f;
            go.transform.position = destination.transform.position;
        }
    }

    public void StartInteraction(GameObject interactor)
    {
        Teleport(interactor);
    }
    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Medium;
    }
}