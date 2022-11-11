using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalGameData
{
    public static PlayerStats playerStats;
    public static float portalCooldown;

    public static int currentDay;
    public static float currentTime;

    public static void Reset()
    {
        playerStats = null;
        portalCooldown = 0;

        currentDay = 1;
        currentTime = 0;
    }
}
