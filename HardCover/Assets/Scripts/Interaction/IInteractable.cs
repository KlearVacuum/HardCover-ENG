using UnityEngine;

public interface IInteractable
{
    void StartInteraction(GameObject interactor);
}

public interface IToggleInteractable : IInteractable
{
    void EndInteraction();
    bool IsInteracting();
}

public interface IActionable
{
    void StartAction();
    void EndAction();
    bool IsActioning();
}

public interface IInteractableAndActionable : IToggleInteractable, IActionable
{
}