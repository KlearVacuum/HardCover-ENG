using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{
    public Portal destination;

    public void Teleport(GameObject go)
    {
        GlobalGameData.portalCooldown = 0.5f;
        go.transform.position = destination.transform.position;
    }

    public void StartInteraction(GameObject interactor)
    {
        if (GlobalGameData.portalCooldown <= 0 && interactor != null)
        {
            Teleport(interactor);
        }
    }

    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Medium;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}