using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcChat : MonoBehaviour, IInteractable
{
    public string npcName = "Something Something";

    public void StartInteraction(GameObject interactor)
    {
        if (true)
        {
            GlobalGameData.dialogManager.StartChat(npcName, "Default");
        }
    }

    public InteractionPriority GetPriority()
    {
        return InteractionPriority.High;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}