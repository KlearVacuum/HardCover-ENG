using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    public TextMeshProUGUI dayNumText;
    public TextMeshProUGUI dayNameText;
    public TextMeshProUGUI timeText;

    public int StartTime = 5;
    public int StartDay = 1;

    private int mTime = 0;
    private int mDay = 0;

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
        AddTime(timeAdded);
        return timeAdded;
    }

    public void AddTime(int hours = 1)
    {
        mTime += hours;
        while (mTime >= 24)
        {
            mTime -= 24;
            ++mDay;
        }

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

    private void UpdateUi()
    {
        dayNumText.text = mDay.ToString();
        dayNameText.text = dayNameMap[mDay];
        timeText.text = $"{mTime:D2}:00";
    }
}