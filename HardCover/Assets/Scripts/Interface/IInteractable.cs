﻿public interface IInteractable
{
    void StartInteraction();
}

public interface IActionable
{
    void StartAction();
    void EndAction();
    bool IsActioning();
}

public interface IInteractableAndActionable : IInteractable, IActionable
{
    void EndInteraction();
    bool IsInteracting();
}