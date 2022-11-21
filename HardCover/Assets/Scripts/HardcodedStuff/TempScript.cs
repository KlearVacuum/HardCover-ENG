using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
{
    public string Speaker;
    public string Key;

    public bool TriggerDialog = false;

    private void Update()
    {
        if (TriggerDialog)
        {
            TriggerDialog = false;
            GlobalGameData.dialogManager.StartChat(Speaker, Key);
        }
    }
}