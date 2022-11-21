using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceDialogManager : MonoBehaviour
{
    [SerializeField] private ChoiceChatbox mChatBox;
    private int mCurrentProgress = 0;

    public List<LineOfDialog> FullChatExposed = new();

    private readonly Dictionary<string, List<Dialog>> FullChat = new();
    private readonly Dictionary<string, Sprite> IconDictionary = new();

    private string mCurrentKey = null;

    void Awake()
    {
        GlobalGameData.choiceDialogManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Pair p in GlobalGameData.dialogManager.IconList)
        {
            IconDictionary[p.Name] = p.Icon;
        }

        foreach (LineOfDialog cd in FullChatExposed)
        {
            FullChat[cd.Key] = cd.Value;
        }
    }

    public void StartChat(string from, string type, int progress = 0)
    {
        mCurrentKey = from + "_" + type;

        if (FullChat.ContainsKey(mCurrentKey))
        {
            GlobalGameData.playerController.DisableMovement();
            mChatBox.EnableChat();

            mCurrentProgress = progress;

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
        else
        {
            FullChat[mCurrentKey][mCurrentProgress++].FunctionToInvoke?.Invoke(false, 0);

            if (mCurrentProgress < FullChat[mCurrentKey].Count - 1)
            {
                ProgressChat();
            }
            else if (mCurrentProgress < FullChat[mCurrentKey].Count)
            {
                if (FullChat[mCurrentKey][mCurrentProgress].Text == "" &&
                    FullChat[mCurrentKey][mCurrentProgress].Speaker == "")
                {
                    var func = FullChat[mCurrentKey][mCurrentProgress].FunctionToInvoke;
                    EndChat();
                    func?.Invoke(false, 0);
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
        Dialog d = FullChat[mCurrentKey][mCurrentProgress];

        mChatBox.SetSpeaker(IconDictionary[d.Speaker], d.Speaker);
        mChatBox.SetText(d.Text);
    }
}