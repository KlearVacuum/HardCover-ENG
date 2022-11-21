using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
{
    public string Speaker;
    public string Key;

    public bool TriggerDialog = false;
    public bool TriggerChoicedDialog = false;

    private void Update()
    {
        if (TriggerDialog)
        {
            TriggerDialog = false;
            GlobalGameData.dialogManager.StartChat(Speaker, Key);
        }

        if (TriggerChoicedDialog)
        {
            TriggerChoicedDialog = false;
            GlobalGameData.choiceDialogManager.StartChat(Speaker, Key);
        }
    }

    public void MarriageDecision(bool choiced, int choice)
    {
        if (choiced)
        {
            if (choice == 1)
            {
                Debug.Log("GET MARRIED OFF");
            }
            else if (choice == 2)
            {
                Debug.Log("STRONG INDEPENDENT WOMAN");
            }
        }
    }

    public void GetFired(bool choiced, int choice)
    {
        Debug.Log("TRIGGER FIRED CUTSCENE");
    }
}