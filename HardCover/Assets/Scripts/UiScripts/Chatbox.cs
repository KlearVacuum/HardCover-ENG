using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chatbox : MonoBehaviour
{
    public GameObject backGround;
    public Image iconImage;
    public TextMeshProUGUI npcText, npcName;

    public void StartChat(NpcChat npc)
    {
        backGround.gameObject.SetActive(true);

        iconImage.sprite = npc.npcIconSprite;
        npcText.text = "Text from " + npc.GetName();
        npcName.text = npc.GetName();
    }

    public void StopChat()
    {
        backGround.gameObject.SetActive(false);
    }
}