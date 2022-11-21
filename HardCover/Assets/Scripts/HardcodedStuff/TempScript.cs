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
                GlobalGameData.blackScreenOverlay.GoToScene("End_Marry");
            }
            else if (choice == 2)
            {
            }
        }
    }

    public void GetFired(bool choiced, int choice)
    {
        GlobalGameData.blackScreenOverlay.GoToScene("End_Fired");
    }

    public void BribedNpc(bool choiced, int choice)
    {
        StartCoroutine(OtherActions());
    }

    private IEnumerator OtherActions()
    {
        // TRIGGER DIALOGUE
        // WAIT FOR PLAYER TO PRESS "BRIBE" (CONTINUE)
        //yield return new WaitForSeconds(1f);

        GlobalGameData.playerController.DisableMovement();

        // Debug.Log("player pays a fine");
        GlobalGameData.playerStats.PayBribe();
        yield return new WaitForSeconds(1f);

        // screen fades to black
        GlobalGameData.blackScreenOverlay.FadeIn(0, false);
        yield return new WaitForSeconds(1.5f);

        // time skip
        // Debug.Log("time skip");
        GlobalGameData.playerStats.PenaltyTimeskip();
        foreach (var npc in GlobalGameData.allNPCs) npc.TeleportToPatrolPoint();

        // screen fades back to normal
        // Debug.Log("screen is fading back");
        GlobalGameData.blackScreenOverlay.FadeOut(0.5f);
        yield return new WaitForSeconds(1.5f);

        foreach (var npc in GlobalGameData.allNPCs)
        {
            npc.currentMaxSpeed = npc.maxSpeed;
            npc.catchPlayer = false;
        }

        // PLAYER DROPS BOOK (BACK IN SHELF?)
        IInteractable ii = GlobalGameData.playerController.GetComponent<InteractionController>().GetBook();
        if (ii is Book b)
        {
            b.ResetPosition();
        }

        // game resumes
        // Debug.Log("resume game");
        GlobalGameData.playerController.EnableMovement();
    }
}