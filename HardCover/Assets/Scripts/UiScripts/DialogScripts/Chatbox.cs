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

    public void SetSpeaker(Sprite icon, string speakerName)
    {
        iconImage.sprite = icon;
        npcName.text = speakerName;
    }

    public void SetText(string t)
    {
        theEffect.StartEffect(t);
    }

    public bool SkipEffect()
    {
        return theEffect.FinishEffect();
    }

    public void EnableChat()
    {
        backGround.gameObject.SetActive(true);
    }

    public void DisableChat()
    {
        backGround.gameObject.SetActive(false);
    }
}