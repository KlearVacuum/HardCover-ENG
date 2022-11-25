using System.Collections;
using System.Collections.Generic;
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

    private int mKnowledge = 20;
    private int mEnergy = 0;
    private int mCash = 0;

    //Getters
    public int knowledge => mKnowledge;
    public int energy => mEnergy;
    public int cash => mCash;

    private void Awake()
    {
        GlobalGameData.playerStats = this;
    }

    private void Start()
    {
        AdjustKnowledge(0, false);
        AdjustEnergy(100, false);
        AdjustCash(150, false);
    }

    public void AdjustKnowledge(int value, bool pop = true)
    {
        int initKnowledge = mKnowledge;
        mKnowledge = Mathf.Clamp(mKnowledge + value, 0, 100);
        value = mKnowledge - initKnowledge;
        knowledgeUI.text = $"{mKnowledge}%";

        switch (mKnowledge)
        {
            case 100:
                // stop reading and drop book
                GlobalGameData.dialogManager.StartChat("Amanda", "100");
                break;
            case 75:
                // stop reading and drop book
                GlobalGameData.dialogManager.StartChat("Amanda", "75");
                break;
            case 50:
                // stop reading and drop book
                GlobalGameData.dialogManager.StartChat("Amanda", "50");
                break;
            case 25:
                // stop reading and drop book
                GlobalGameData.dialogManager.StartChat("Amanda", "25");
                break;
        }

        if (!pop)
        {
            return;
        }

        if (value >= 0)
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"+{value}%", StatAdjustColors.KnowledgeIncrease);
        }
        else
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"{value}%", StatAdjustColors.KnowledgeDecrease);
        }
    }

    public void AdjustEnergy(int value, bool pop = true)
    {
        int initEnergy = mEnergy;
        mEnergy = Mathf.Clamp(mEnergy + value, 0, 100);
        value = mEnergy - initEnergy;
        energyUI.text = $"{mEnergy}%";

        if (mEnergy == 0)
        {
            GlobalGameData.dialogManager.StartChat("Amanda", "LowEnergy");
        }

        if (!pop)
        {
            return;
        }

        if (value >= 0)
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"+{value}% Energy", StatAdjustColors.EnergyIncrease);
        }
        else
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"{value}% Energy", StatAdjustColors.EnergyDecrease);
        }
    }

    public void AdjustCash(int value, bool pop = true)
    {
        int initCash = mCash;
        mCash = Mathf.Max(mCash + value, 0);
        value = mCash - initCash;
        cashUI.text = $"${mCash}";

        if (mCash <= 50)
        {
            GlobalGameData.dialogManager.StartChat("Amanda", "LowMoney");
        }

        if (!pop)
        {
            return;
        }

        if (value >= 0)
        {
            GlobalGameData.PopInPopOutValue(transform.position, $"+${value}", StatAdjustColors.CashIncrease);
        }
        else
        {
            value *= -1;
            GlobalGameData.PopInPopOutValue(transform.position, $"-${value}", StatAdjustColors.CashDecrease);
        }
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

    public bool CanPassUni()
    {
        return mKnowledge == 100 && mEnergy >= 60 && mCash >= 200;
    }

    public bool TryPurchase(int cost, bool pop = true)
    {
        if (mCash >= cost)
        {
            AdjustCash(-cost, pop);
            return true;
        }

        return false;
    }

    public bool TryConsumeEnergy(int energyCost, bool pop = true)
    {
        if (mEnergy >= energyCost)
        {
            AdjustEnergy(-energyCost, pop);
            return true;
        }

        return false;
    }

    public int JustConsumeEnergy(int energyCost, bool pop = true)
    {
        if (mEnergy >= energyCost)
        {
            AdjustEnergy(-energyCost, pop);
            return energyCost;
        }
        else
        {
            AdjustEnergy(-mEnergy, pop);
            return mEnergy;
        }
    }

    public void PayBribe()
    {
        AdjustCash(-8);
    }

    public void PenaltyTimeskip()
    {
        GlobalGameData.timeManager.AddTime();
    }
}