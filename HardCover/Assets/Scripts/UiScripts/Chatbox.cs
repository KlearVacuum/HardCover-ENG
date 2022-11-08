using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chatbox : MonoBehaviour
{
    public GameObject backGround;
    public Image iconImage;
    public TextMeshProUGUI npcText, npcName;
    private TypeWriterEffect theEffect;

    private void Awake()
    {
        theEffect = npcText.GetComponent<TypeWriterEffect>();
    }

    public void StartChat(NpcChat npc)
    {
        backGround.gameObject.SetActive(true);

        iconImage.sprite = npc.npcIconSprite;
        npcName.text = npc.GetName();
        theEffect?.StartEffect("Something something something");
    }

    public void StopChat()
    {
        backGround.gameObject.SetActive(false);
    }
}