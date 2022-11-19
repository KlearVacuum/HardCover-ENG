using UnityEngine;

public static class GlobalGameData
{
    public static PlayerStats playerStats;
    public static DayNightCycleManager timeManager;
    public static float portalCooldown;

    public static int currentDay;
    public static float currentTime;

    public static void Reset()
    {
        playerStats = null;
        timeManager = null;
        portalCooldown = 0;

        currentDay = 1;
        currentTime = 0;
    }

    public static void PopInPopOutValue(Vector3 location, string text)
    {
        GameObject.Instantiate(playerStats.PopInPopOutObject,
            location - ValuePopInPopOut.sDown,
            Quaternion.identity).GetComponent<ValuePopInPopOut>().SetText(text);
    }
}