using TMPro;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    private int mTime = 0;
    private int mDay = 0;

    // Start is called before the first frame update
    void Start()
    {
        GlobalGameData.timeManager = this;
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
        textBox.text = $"Day: {mDay}\t\t\t Time: {mTime}";
    }
}