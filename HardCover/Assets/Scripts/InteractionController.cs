using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public IInteractable interactableObj;

    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode actionKey = KeyCode.Space;

    void Update()
    {
        if (interactableObj != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (interactableObj is IInteractableAndActionable iia)
                {
                    if (iia.IsInteracting())
                    {
                        iia.EndInteraction();
                    }
                    else
                    {
                        iia.StartInteraction();
                    }
                }
                else
                {
                    interactableObj.StartInteraction();
                }
            }
            else if (Input.GetKeyDown(actionKey))
            {
                if (interactableObj is IInteractableAndActionable iia)
                {
                    if (iia.IsActioning())
                    {
                        iia.EndAction();
                    }
                    else
                    {
                        iia.StartAction();
                    }
                }
            }
        }
    }
}