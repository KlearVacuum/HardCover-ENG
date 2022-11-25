using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycleManager : MonoBehaviour
{
    public TextMeshProUGUI dayNumText;
    public TextMeshProUGUI dayNameText;
    public TextMeshProUGUI timeText;
    public Light2D globalLight;

    public int StartTime = 5;
    public int StartDay = 1;

    public Color daySkyColor;
    public Color eveningSkyColor;
    public Color nightSkyColor;
    public float skyColorLerpSpeed;

    private int mTime = 0;
    private int mDay = 0;

    public static int WakeUpTime = 6;
    public static int WorkStartTime = 6;
    public static int WorkEndTime = 18;
    public static int SleepTime = 22;

    private static readonly Dictionary<int, string> dayNameMap
        = new Dictionary<int, string>()
        {
            { 1, "Monday" },
            { 2, "Tuesday" },
            { 3, "Wednesday" },
            { 4, "Thursday" },
            { 5, "Friday" },
            { 6, "Saturday" },
            { 7, "Sunday" },
            { 8, "Monday" },
            { 9, "Tuesday" },
            { 10, "Wednesday" },
            { 11, "Thursday" },
            { 12, "Friday" },
            { 13, "Saturday" },
            { 14, "Sunday" },
            { 15, "Monday" }
        };

    // Start is called before the first frame update
    void Start()
    {
        GlobalGameData.timeManager = this;
        mTime = StartTime;
        mDay = StartDay;
        UpdateUi();
        foreach (var npc in GlobalGameData.allNPCs) npc.canTransit = true;
    }

    private void Update()
    {
        SkyColorLerp();
        GlobalLightLerp();
    }

    void GlobalLightLerp()
    {
        float desiredIntensity;

        // between 12pm to 6pm, full strength
        if (mTime > 12 && mTime <= 18)
        {
            desiredIntensity = 0.8f;
        }
        // between 6pm to 9pm, dim
        else if (mTime > 18 && mTime <= 21)
        {
            float t = (float)((mTime - 18) / (21f - 18f));
            desiredIntensity = Mathf.Lerp(0.8f, 0.3f, t);
        }
        // between 6am to 12pm, brighten
        else if (mTime > 6 && mTime <= 12)
        {
            float t = (float)((mTime - 6) / (12f - 6f));
            desiredIntensity = Mathf.Lerp(0.3f, 0.8f, t);
        }
        else desiredIntensity = 0.3f;

        if (Mathf.Abs(globalLight.intensity - desiredIntensity) <= 0.0001f) return;
        globalLight.intensity += (desiredIntensity - globalLight.intensity) * Time.deltaTime * skyColorLerpSpeed;
    }

    void SkyColorLerp()
    {
        Color currColor = Camera.main.backgroundColor;
        Color desiredColor = GetDesiredSkyColor();

        if (Vector4.Distance(currColor, desiredColor) <= 0.02f) return;

        Camera.main.backgroundColor += new Color(desiredColor.r - currColor.r, desiredColor.g - currColor.g, desiredColor.b - currColor.b, desiredColor.a - currColor.a) * Time.deltaTime * skyColorLerpSpeed;
    }

    Color GetDesiredSkyColor()
    {
        // 6am to 3pm
        if (mTime > 6 && mTime <= 15)
        {
            float t = (float)((mTime - 6f) / (15f - 6f));
            return Vector4.Lerp(nightSkyColor, daySkyColor, t);
        }

        // 3pm to 5pm
        else if (mTime > 15 && mTime <= 17)
        {
            float t = (float)((mTime - 15f) / (17f - 15f));
            return Vector4.Lerp(daySkyColor, eveningSkyColor, t);
        }

        // 5pm to 9pm
        else if (mTime > 17 && mTime <= 21)
        {
            float t = (float)((mTime - 17f) / (21f - 17f));
            return Vector4.Lerp(eveningSkyColor, nightSkyColor, t);
        }

        return nightSkyColor;
    }


    public int HoursPassedFrom(int timeFrom)
    {
        int timePassed = 0;
        int temp = mTime;

        while (temp != timeFrom)
        {
            --temp;
            ++timePassed;

            while (temp < 0)
            {
                temp += 24;
            }
        }

        return timePassed;
    }

    public int HoursLeftTill(int timeTo)
    {
        int timeToAdd = 0;
        int temp = mTime;

        while (temp != timeTo)
        {
            ++temp;
            ++timeToAdd;

            while (temp >= 24)
            {
                temp -= 24;
            }
        }

        return timeToAdd;
    }

    // Returns how many hours it was
    public int AddTimeUntil(int timeToAddUntil)
    {
        int timeAdded = HoursLeftTill(timeToAddUntil);
        StartCoroutine(TimeSkipCoroutine(timeAdded));
        return timeAdded;
    }

    IEnumerator TimeSkipCoroutine(int timeAdded)
    {
        GlobalGameData.blackScreenOverlay.FadeIn(0.5f, false);
        GlobalGameData.playerController.DisableMovement();
        yield return new WaitForSeconds(2f);

        foreach (var npc in GlobalGameData.allNPCs) npc.TeleportToPatrolPoint();
        AddTime(timeAdded);
        GlobalGameData.blackScreenOverlay.FadeOut(0.5f);
        yield return new WaitForSeconds(1.5f);

        GlobalGameData.playerController.EnableMovement();
    }

    public void AddTime(int hours = 1)
    {
        mTime += hours;
        while (mTime >= 24)
        {
            mTime -= 24;
            ++mDay;
        }

        if (mDay == 15 && mTime == 6)
        {
            if (!GlobalGameData.playerStats.CanPassUni())
            {
                GlobalGameData.blackScreenOverlay.GoToScene("End_UniFail");
            }
        }

        foreach (var npc in GlobalGameData.allNPCs) npc.canTransit = true;
        UpdateUi();
    }

    public int GetTime()
    {
        return mTime;
    }

    public int GetDay()
    {
        return mDay;
    }

    public string GetDayName()
    {
        return dayNameMap[mDay];
    }

    public bool IsBeforeWork()
    {
        return mDay % 7 != 0 &&
               (mTime >= SleepTime || mTime < WorkStartTime);
    }

    public bool IsDuringWork()
    {
        return mDay % 7 != 0 &&
               (mTime >= WorkStartTime && mTime < WorkEndTime);
    }

    public bool IsAfterWork()
    {
        return mDay % 7 != 0 &&
               (mTime >= WorkEndTime || mTime < SleepTime);
    }

    private void UpdateUi()
    {
        dayNumText.text = mDay.ToString();
        dayNameText.text = dayNameMap[mDay];
        timeText.text = $"{mTime:D2}:00";
    }
}