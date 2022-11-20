using System.Collections.Generic;
using UnityEngine;

public class ChoiceDialogManager : MonoBehaviour
{
    [SerializeField] private ChoiceChatbox mChatBox;

    public List<LineOfDialog> FullChatExposed = new();

    private readonly Dictionary<string, List<Dialog>> FullChat = new();
    private readonly Dictionary<string, Sprite> IconDictionary = new();

    private string mCurrentKey = null;
    private int mCurrentProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        GlobalGameData.choiceDialogManager = this;

        foreach (Pair p in GlobalGameData.dialogManager.IconList)
        {
            IconDictionary[p.Name] = p.Icon;
        }

        foreach (LineOfDialog cd in FullChatExposed)
        {
            FullChat[cd.Key] = cd.Value;
        }
    }

    public void StartChat(string from, string type)
    {
        mCurrentKey = from + "_" + type;

        if (FullChat.ContainsKey(mCurrentKey))
        {
            GlobalGameData.playerController.DisableMovement();
            mChatBox.EnableChat();

            mCurrentProgress = 0;

            ProgressChat();
        }
        else
        {
            EndChat();
        }
    }

    public void NextButton()
    {
        if (mChatBox.SkipEffect())
        {
            // need to press next button again to proceed
        }
        else if (mCurrentProgress < FullChat[mCurrentKey].Count - 2)
        {
            FullChat[mCurrentKey][mCurrentProgress].FunctionToInvoke?.Invoke(false, 0);
            ProgressChat();
        }
        else if (mCurrentProgress == FullChat[mCurrentKey].Count - 1)
        {
            if (FullChat[mCurrentKey][mCurrentProgress].Text == "" &&
                FullChat[mCurrentKey][mCurrentProgress].Speaker == "")
            {
                EndChat();
            }
            else
            {
                mChatBox.SetChoiceText(
                    FullChat[mCurrentKey][mCurrentProgress].Speaker,
                    FullChat[mCurrentKey][mCurrentProgress].Text
                );

                mChatBox.EnableChoice();
            }
        }
        else
        {
            EndChat();
        }
    }

    public void ChoiceOne()
    {
        mChatBox.DisableChoice();
        FullChat[mCurrentKey][mCurrentProgress].FunctionToInvoke?.Invoke(true, 1);
        StartChat(mCurrentKey, "1");
    }

    public void ChoiceTwo()
    {
        mChatBox.DisableChoice();
        FullChat[mCurrentKey][mCurrentProgress].FunctionToInvoke?.Invoke(true, 2);
        StartChat(mCurrentKey, "2");
    }

    public void EndChat()
    {
        mCurrentKey = null;
        mCurrentProgress = 0;
        mChatBox.DisableChat();
        GlobalGameData.playerController.EnableMovement();
    }

    private void ProgressChat()
    {
        Dialog d = FullChat[mCurrentKey][mCurrentProgress++];

        mChatBox.SetSpeaker(IconDictionary[d.Speaker], d.Speaker);
        mChatBox.SetText(d.Text);
    }
}