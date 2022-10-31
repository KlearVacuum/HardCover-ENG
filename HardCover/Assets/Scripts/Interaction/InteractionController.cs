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
                if (interactableObj is IInteractableAndActionable iia) // ToggleInteractable and Actionable
                {
                    if (!iia.IsActioning())
                    {
                        ToggleInteraction(iia);
                    }
                }
                else if (interactableObj is IToggleInteractable iti) // ToggleInteractable
                {
                    ToggleInteraction(iti);
                }
                else // Only Interactable
                {
                    interactableObj.StartInteraction(gameObject);
                }
            }
            else if (Input.GetKeyDown(actionKey))
            {
                if (interactableObj is IInteractableAndActionable iia) // ToggleInteractable and Actionable
                {
                    ToggleAction(iia);
                }
            }
        }
    }

    void ToggleInteraction(IToggleInteractable iti)
    {
        if (iti.IsInteracting())
        {
            iti.EndInteraction();
        }
        else
        {
            iti.StartInteraction(gameObject);
        }
    }

    void ToggleAction(IInteractableAndActionable iia)
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