using UnityEngine;
using UnityEngine.UI;

public class NpcChat : MonoBehaviour, IInteractable
{
    public Sprite npcIconSprite;
    public string npcName = "Something Something";

    public Chatbox chatObject;

    public void StartInteraction(GameObject interactor)
    {
        if (true)
        {
            chatObject.StartChat(this);
            GlobalGameData.playerController.DisableMovement();
        }
    }

    public string GetName()
    {
        return npcName;
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