using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcChat : MonoBehaviour, IInteractable
{
    public string npcName = "Something Something";

    public void StartInteraction(GameObject interactor)
    {
        if (GlobalGameData.timeManager.IsBeforeWork())
        {
            GlobalGameData.dialogManager.StartChat(npcName, "BeforeWork");
        }
        else if (GlobalGameData.timeManager.IsDuringWork())
        {
            GlobalGameData.dialogManager.StartChat(npcName, "DuringWork");
        }
        else if (GlobalGameData.timeManager.IsAfterWork())
        {
            GlobalGameData.dialogManager.StartChat(npcName, "AfterWork");
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