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
        }
    }

    public string GetName()
    {
        return npcName;
    }
}