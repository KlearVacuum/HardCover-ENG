using UnityEngine;

public class Portal : MonoBehaviour, IInteractable
{
    public Transform destination;

    public void Teleport(GameObject go)
    {
        if (GlobalGameData.portalCooldown <= 0 && go != null)
        {
            GlobalGameData.portalCooldown = 0.5f;
            go.transform.position = destination.position;
        }
    }

    public void StartInteraction(GameObject interactor)
    {
        Teleport(interactor);
    }
}