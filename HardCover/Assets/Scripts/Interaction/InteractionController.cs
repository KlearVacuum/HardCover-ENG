using UnityEngine;

// On Player
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
                    if (!iia.IsActioning())
                    {
                        if (iia.IsInteracting())
                        {
                            iia.EndInteraction();
                        }
                        else
                        {
                            iia.StartInteraction(gameObject);
                        }
                    }
                }
                else
                {
                    interactableObj.StartInteraction(gameObject);
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
                    else if (iia.IsInteracting())
                    {
                        iia.StartAction();
                    }
                }
            }
        }
    }
}