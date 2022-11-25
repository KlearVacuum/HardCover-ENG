using UnityEngine;

public class WorkTable : MonoBehaviour, IInteractableAndActionable
{
    public int WorkTimeStart = DayNightCycleManager.WorkStartTime; // Work starts 6am
    public int WorkTimeEnd = DayNightCycleManager.WorkEndTime; // Work ends 6pm

    public int EnergyPerHour = 4; // 4% energy cost
    public int IncomePerHour = 1; // $1

    private bool isInteracting;

    private bool mMarryTrigger = false;

    public void StartInteraction(GameObject interactor)
    {
        // Sit at workbench
        isInteracting = true;
        GlobalGameData.playerController.DisableMovement();
    }

    public void EndInteraction()
    {
        // Stand up from workbench
        isInteracting = false;
        GlobalGameData.playerController.EnableMovement();
    }

    public void StartAction()
    {
        int currentTime = GlobalGameData.timeManager.GetTime();

        if (currentTime == 5)
        {
            //TODO:FIX
            GlobalGameData.timeManager.AddTime();
            ++currentTime;
        }

        if (currentTime >= WorkTimeStart && currentTime < WorkTimeEnd)
        {
            // Start working

            // Time Progress
            GlobalGameData.timeManager.HoursSkipped(GlobalGameData.timeManager.HoursPassedFrom(WorkTimeStart));
            int hoursWorked = GlobalGameData.timeManager.AddTimeUntil(WorkTimeEnd);

            // Money Progress
            hoursWorked = GlobalGameData.playerStats.JustConsumeEnergy(hoursWorked * EnergyPerHour);
            GlobalGameData.playerStats.AdjustCash(hoursWorked * IncomePerHour);
        }
        else
        {
            Debug.Log("Not Working Hours");
        }

        int workSkipped = GlobalGameData.timeManager.GetHoursSkipped();

        if (workSkipped >= 36 && !GlobalGameData.timeManager.m36Trigger)
        {
            GlobalGameData.timeManager.m36Trigger = true;
            GlobalGameData.dialogManager.StartChat("Ting Hoon", "AmandaSkipWork36");
        }
        else if (workSkipped >= 24 && !GlobalGameData.timeManager.m24Trigger)
        {
            GlobalGameData.timeManager.m24Trigger = true;
            GlobalGameData.dialogManager.StartChat("Ting Hoon", "AmandaSkipWork24");
        }
        else if (workSkipped >= 12 && !GlobalGameData.timeManager.m12Trigger)
        {
            GlobalGameData.timeManager.m12Trigger = true;
            GlobalGameData.dialogManager.StartChat("Ting Hoon", "AmandaSkipWork12");
        }

        if (GlobalGameData.playerStats.knowledge > 50 && !mMarryTrigger)
        {
            mMarryTrigger = true;
            GlobalGameData.choiceDialogManager.StartChat("Ting Hoon", "ProposeMarriage");
        }
    }

    public void EndAction()
    {
        //Blank
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }

    public bool IsActioning()
    {
        return false;
    }

    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Default;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}