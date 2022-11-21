using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceChatbox : MonoBehaviour
{
    public GameObject backGround;
    public Image iconImage;
    public TextMeshProUGUI npcText, npcName;

    public GameObject choiceObject;
    public TextMeshProUGUI choiceOne, choiceTwo;

    public Button nextButton;

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

    public void EnableChoice()
    {
        choiceObject.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
    }

    public void DisableChoice()
    {
        choiceObject.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    public void SetChoiceText(string oneText, string twoText)
    {
        choiceOne.text = oneText;
        choiceTwo.text = twoText;
    }
}