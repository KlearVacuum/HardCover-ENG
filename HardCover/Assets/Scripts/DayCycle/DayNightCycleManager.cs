using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    private uint mTime = 0;
    private uint mDay = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUi();
    }

    public void AddTime(uint time = 1)
    {
        mTime += time;
        while (mTime > 23)
        {
            mTime -= 23;
            ++mDay;
        }

        UpdateUi();
    }

    public uint GetTime()
    {
        return mTime;
    }

    public uint GetDay()
    {
        return mDay;
    }

    private void UpdateUi()
    {
    }
}