using UnityEngine;

public class WorkTable : MonoBehaviour, IInteractableAndActionable
{
    public int WorkTimeStart = 6; // Work starts 6am
    public int WorkTimeEnd = 18; // Work ends 6pm

    public int EnergyPerHour = 4; // 4% energy cost
    public int IncomePerHour = 1; // $1

    private bool isInteracting;

    private int mHoursSkipped = 0;

    public void StartInteraction(GameObject interactor)
    {
        // Sit at workbench
        isInteracting = true;
        Debug.Log("Sit at workbench");
    }

    public void EndInteraction()
    {
        // Stand up from workbench
        isInteracting = false;
        Debug.Log("Stand up from workbench");
    }

    public void StartAction()
    {
        int currentTime = GlobalGameData.timeManager.GetTime();

        if (currentTime >= WorkTimeStart && currentTime < WorkTimeEnd)
        {
            // Start working
            Debug.Log("Work and Start TimeSkip");

            // Time Progress
            mHoursSkipped += GlobalGameData.timeManager.HoursPassedFrom(WorkTimeStart);
            int hoursWorked = GlobalGameData.timeManager.AddTimeUntil(WorkTimeEnd);

            // Money Progress
            GlobalGameData.playerStats.cash += hoursWorked * IncomePerHour;
        }
        else
        {
            Debug.Log("Not Working Hours");
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
}