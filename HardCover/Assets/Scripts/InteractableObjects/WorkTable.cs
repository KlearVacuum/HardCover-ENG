using UnityEngine;

public class WorkTable : MonoBehaviour, IInteractableAndActionable
{
    public int WorkTimeStart = DayNightCycleManager.WorkStartTime; // Work starts 6am
    public int WorkTimeEnd = DayNightCycleManager.WorkEndTime; // Work ends 6pm

    public int EnergyPerHour = 4; // 4% energy cost
    public int IncomePerHour = 1; // $1

    private bool isInteracting;

    private int mHoursSkipped = 0;
    private bool m12Trigger = false;
    private bool m24Trigger = false;
    private bool m36Trigger = false;
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
            mHoursSkipped += GlobalGameData.timeManager.HoursPassedFrom(WorkTimeStart);
            int hoursWorked = GlobalGameData.timeManager.AddTimeUntil(WorkTimeEnd);

            // Money Progress
            hoursWorked = GlobalGameData.playerStats.JustConsumeEnergy(hoursWorked * -EnergyPerHour);
            GlobalGameData.playerStats.AdjustCash(hoursWorked * IncomePerHour);
        }
        else
        {
            Debug.Log("Not Working Hours");
        }

        if (mHoursSkipped >= 36 && !m36Trigger)
        {
            m36Trigger = true;
            GlobalGameData.dialogManager.StartChat("Ting Hoon", "AmandaSkipWork36");
        }
        else if (mHoursSkipped >= 24 && !m24Trigger)
        {
            m24Trigger = true;
            GlobalGameData.dialogManager.StartChat("Ting Hoon", "AmandaSkipWork24");
        }
        else if (mHoursSkipped >= 12 && !m12Trigger)
        {
            m12Trigger = true;
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