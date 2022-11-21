using UnityEngine;

public class Bed : MonoBehaviour, IInteractableAndActionable
{
    public int EnergyRecoveryPerHour = 5;
    public int WakeUpTime = DayNightCycleManager.WakeUpTime; // 5am Wakeup time
    public int OverSleepTime = 3;

    public GameObject Orientation;

    public int GetTimeToSleepUntil()
    {
        int timeToSleepUntil;

        int time = GlobalGameData.timeManager.GetTime();

        int startingEnergy = GlobalGameData.playerStats.energy;
        int missingEnergy = 100 - startingEnergy;
        int hoursRequired = missingEnergy == 0 ? 0 : missingEnergy / EnergyRecoveryPerHour + 1;

        int tillWakeUpTime = GlobalGameData.timeManager.HoursLeftTill(WakeUpTime);
        int difference = hoursRequired - tillWakeUpTime;

        if (time == WakeUpTime && hoursRequired == 0)
        {
            timeToSleepUntil = time + OverSleepTime;
        }
        else if (difference >= 0) // Need to sleep more then you have time for or Just nice
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

        if (timeToSleepUntil >= 24)
        {
            timeToSleepUntil -= 24;
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
        GlobalGameData.playerController.DisableMovement();
        GlobalGameData.playerController.SetPositionAndRotation(Orientation.transform.position,
            Orientation.transform.rotation);
    }

    public void EndInteraction()
    {
        isInteracting = false;
        GlobalGameData.playerController.EnableMovement();
        GlobalGameData.playerController.ResetPositionAndRotation();
    }

    public void StartAction()
    {
        int timeToSleepUntil = GetTimeToSleepUntil();

        int hoursSlept = GlobalGameData.timeManager.AddTimeUntil(timeToSleepUntil);
        GlobalGameData.playerStats.AdjustEnergy(hoursSlept * EnergyRecoveryPerHour);

        EndInteraction();
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
        return isInteracting ? InteractionPriority.High : InteractionPriority.Default;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}