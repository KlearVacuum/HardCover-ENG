﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// On Player
public class InteractionController : MonoBehaviour
{
    public List<IInteractable> interactableObj = new List<IInteractable>();

    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode actionKey = KeyCode.Space;

    void Update()
    {
        if (interactableObj.Count > 0)
        {
            IInteractable top = interactableObj.Last();

            if (Input.GetKeyDown(interactKey))
            {
                if (top is IInteractableAndActionable iia) // ToggleInteractable and Actionable
                {
                    if (!iia.IsActioning())
                    {
                        ToggleInteraction(iia);
                    }
                }
                else if (top is IToggleInteractable iti) // ToggleInteractable
                {
                    ToggleInteraction(iti);
                }
                else // Only Interactable
                {
                    top.StartInteraction(gameObject);
                }
            }
            else if (Input.GetKeyDown(actionKey))
            {
                if (top is IInteractableAndActionable iia) // ToggleInteractable and Actionable
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