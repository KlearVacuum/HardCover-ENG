using UnityEngine;

public enum InteractionPriority
{
    Default = 0,
    Low = 1,
    Medium = 2,
    High = 3
}

public interface IInteractable
{
    void StartInteraction(GameObject interactor);
    InteractionPriority GetPriority();
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