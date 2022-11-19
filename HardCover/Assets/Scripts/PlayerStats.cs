using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public struct StatAdjustmentColors
{
    public Color KnowledgeIncrease;
    public Color KnowledgeDecrease;

    public Color EnergyIncrease;
    public Color EnergyDecrease;

    public Color CashIncrease;
    public Color CashDecrease;
}


public class PlayerStats : MonoBehaviour
{
    public GameObject playerProgressBarRoot;
    public Image progressBar;

    public GameObject PopInPopOutObject;

    public StatAdjustmentColors StatAdjustColors = new StatAdjustmentColors();

    public TextMeshProUGUI knowledgeUI, energyUI, cashUI;

    private int mKnowledge;
    private int mEnergy;
    private int mCash;

    //Getters
    public int knowledge => mKnowledge;
    public int energy => mEnergy;
    public int cash => mCash;

    private void Start()
    {
        GlobalGameData.playerStats = this;
        mKnowledge = 0;
        mEnergy = 100;
        mCash = 150;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    knowledge+=10;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    energy -= 10;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    cash -= 50;
        //}
    }

    public void AdjustKnowledge(int value)
    {
        int initKnowledge = mKnowledge;
        mKnowledge = Mathf.Clamp(mKnowledge + value, 0, 100);
        value = mKnowledge - initKnowledge;

        if (value >= 0)
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"+{value}%", StatAdjustColors.KnowledgeIncrease);
        }
        else 
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"-{value}%", StatAdjustColors.KnowledgeDecrease);
        }

        knowledgeUI.text = mKnowledge + "% Knowledge Progress";
    }

    public void AdjustEnergy(int value)
    {
        int initEnergy = mEnergy;
        mEnergy = Mathf.Clamp(mEnergy + value, 0, 100);
        value = mEnergy - initEnergy;

        if (value >= 0)
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"+{value}E", StatAdjustColors.EnergyIncrease);
        }
        else
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"-{value}E", StatAdjustColors.EnergyDecrease);
        }

        energyUI.text = mEnergy + "% Energy Remaining";
    }

    public void AdjustCash(int value)
    {
        int initCash = mCash;
        mCash = Mathf.Max(mCash + value, 0);
        value = mCash - initCash;

        if (value >= 0)
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"+${value}", StatAdjustColors.CashIncrease);
        }
        else
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"-${value}", StatAdjustColors.CashDecrease);
        }

        cashUI.text = "$" + mCash;
    }

    public void ShowActionBar()
    {
        playerProgressBarRoot.SetActive(true);
    }

    public void HideActionBar()
    {
        playerProgressBarRoot.SetActive(false);
    }

    public void SetProgress(float val)
    {
        Mathf.Clamp01(val);
        progressBar.fillAmount = val;
    }

    public bool TryPurchase(int cost)
    {
        if (mCash >= cost)
        {
            AdjustCash(-cost);
            return true;
        }

        return false;
    }

    public void TriggerPenalty()
    {
        AdjustCash(-8);
        int time = GlobalGameData.timeManager.GetTime();

        if (time > 18 || time < 5) // Time skip to 5am
        {
            GlobalGameData.timeManager.AddTimeUntil(5);
        }
        else // Time skip to 6pm
        {
            GlobalGameData.timeManager.AddTimeUntil(18);
        }
    }
}