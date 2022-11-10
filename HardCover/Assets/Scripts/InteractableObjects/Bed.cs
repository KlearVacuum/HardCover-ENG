using UnityEngine;

public class Bed : MonoBehaviour, IInteractableAndActionable
{
    public int EnergyRecoveryPerHour = 5;
    public int WakeUpTime = 5; // 5am Wakeup time
    public int OverSleepTime = 3;

    public int GetTimeToSleepUntil()
    {
        int timeToSleepUntil;

        int time = GlobalGameData.timeManager.GetTime();

        int startingEnergy = GlobalGameData.playerStats.energy;
        int missingEnergy = 100 - startingEnergy;
        int hoursRequired = missingEnergy == 0 ? 0 : missingEnergy / EnergyRecoveryPerHour + 1;

        int tillWakeUpTime = GlobalGameData.timeManager.HoursLeftTill(WakeUpTime);
        int difference = hoursRequired - tillWakeUpTime;

        if (difference >= 0) // Need to sleep more then you have time for or Just nice
        {
            // Just sleep till wake up time
            timeToSleepUntil = WakeUpTime;
        }
        else // Have more than enough time to sleep
        {
            hoursRequired += OverSleepTime; // OVERSLEEP SYNDROME

            if (hoursRequired > tillWakeUpTime)
            {
                // Just sleep till wake up time
                timeToSleepUntil = tillWakeUpTime;
            }
            else
            {
                // Sleep till necessary
                timeToSleepUntil = time + hoursRequired;
            }
        }

        return timeToSleepUntil;
    }

    // ========================================================
    // ALL INTERFACE THINGS
    // ========================================================

    // ========================================================
    // IShowUiPopUp
    // ========================================================
    public void TriggerEnter(Collider2D collision)
    {
        Debug.Log("Show Ui");
    }

    public void TriggerExit(Collider2D collision)
    {
        Debug.Log("Hide Ui");
    }

    // ========================================================
    // IInteractableAndActionable
    // ========================================================
    private bool isInteracting;

    public void StartInteraction(GameObject interactor)
    {
        isInteracting = true;
        Debug.Log("Enter Bed");
    }

    public void EndInteraction()
    {
        isInteracting = false;
        Debug.Log("Exit Bed");
    }

    public void StartAction()
    {
        Debug.Log("Start Sleeping");

        int timeToSleepUntil = GetTimeToSleepUntil();

        int hoursSlept = GlobalGameData.timeManager.AddTimeUntil(timeToSleepUntil);
        GlobalGameData.playerStats.energy += hoursSlept * EnergyRecoveryPerHour;
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