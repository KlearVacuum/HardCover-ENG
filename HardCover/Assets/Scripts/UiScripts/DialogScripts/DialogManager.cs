using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Dialog
{
    public string Speaker;
    public string Text;

    //Params are for choice stuff
    // True to make it choiced
    // 1 for choice 1
    // 2 for choice 2
    public UnityEvent<bool, int> FunctionToInvoke;
}

[System.Serializable]
public struct LineOfDialog
{
    public string Key;
    public List<Dialog> Value;
}

[System.Serializable]
public struct Pair
{
    public string Name;
    public Sprite Icon;
}

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Chatbox mChatBox;

    public List<LineOfDialog> FullChatExposed = new();
    public List<Pair> IconList = new();

    private readonly Dictionary<string, List<Dialog>> FullChat = new();
    private readonly Dictionary<string, Sprite> IconDictionary = new();

    private string mCurrentKey = null;
    private int mCurrentProgress = 0;

    void Awake()
    {
        GlobalGameData.dialogManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Pair p in IconList)
        {
            IconDictionary[p.Name] = p.Icon;
        }

        foreach (LineOfDialog lod in FullChatExposed)
        {
            FullChat[lod.Key] = lod.Value;
        }
    }

    public void StartChat(string from, string type)
    {
        GlobalGameData.playerController.DisableMovement();
        mChatBox.EnableChat();

        mCurrentKey = from + "_" + type;
        mCurrentProgress = 0;

        ProgressChat();
    }

    public void NextButton()
    {
        if (mChatBox.SkipEffect())
        {
            // need to press next button again to proceed
        }
        else if (mCurrentProgress < FullChat[mCurrentKey].Count - 1)
        {
            FullChat[mCurrentKey][mCurrentProgress].FunctionToInvoke?.Invoke(false, 0);
            ProgressChat();
        }
        else
        {
            EndChat();
        }
    }

    public void EndChat()
    {
        Debug.Log("Chat Ended");
        mChatBox.DisableChat();
        GlobalGameData.playerController.EnableMovement();
    }

    private void ProgressChat()
    {
        Dialog d = FullChat[mCurrentKey][mCurrentProgress++];

        mChatBox.SetSpeaker(IconDictionary[d.Speaker], d.Speaker);
        mChatBox.SetText(d.Text);

        Debug.Log("Chat Progressed " + mCurrentProgress + " Out of: " + FullChat[mCurrentKey].Count);
    }
}