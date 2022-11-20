using UnityEngine;

public static class GlobalGameData
{
    public static PlayerController playerController;
    public static PlayerStats playerStats;
    public static DayNightCycleManager timeManager;
    public static float portalCooldown;

    public static int currentDay;
    public static float currentTime;

    public static void Reset()
    {
        playerStats = null;
        timeManager = null;
        playerController = null;
        portalCooldown = 0;
    }

    public static void PopInPopOutValue(Vector3 location, string text, Color col)
    {
        GameObject go = GameObject.Instantiate(playerStats.PopInPopOutObject,
            location - ValuePopInPopOut.sDown,
            Quaternion.identity);

        ValuePopInPopOut vpip = go.GetComponent<ValuePopInPopOut>();
        vpip.SetText(text);
        vpip.SetColor(col);
    }
}