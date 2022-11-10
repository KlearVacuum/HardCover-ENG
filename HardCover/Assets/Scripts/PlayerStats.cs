using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public GameObject playerProgressBarRoot;
    public Image progressBar;

    public TextMeshProUGUI knowledgeUI, energyUI, cashUI;

    private int mKnowledge;

    public int knowledge
    {
        get => mKnowledge;
        set
        {
            mKnowledge = Mathf.Clamp(value, 0, 100);
            UpdateKnowledgeUi(mKnowledge);
        }
    }

    private int mEnergy;

    public int energy
    {
        get => mEnergy;
        set
        {
            mEnergy = Mathf.Clamp(value, 0, 100);
            UpdateEnergyUi(mEnergy);
        }
    }

    private int mCash;

    public int cash
    {
        get => mCash;
        set
        {
            mCash = Mathf.Max(value, 0);
            UpdateCashUi(mCash);
        }
    }

    private void Start()
    {
        GlobalGameData.playerStats = this;
        knowledge = 0;
        energy = 100;
        cash = 150;
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

    void UpdateKnowledgeUi(int amount)
    {
        double value = amount;
        knowledgeUI.text = value + "% Knowledge Progress";
    }

    void UpdateEnergyUi(int amount)
    {
        double value = amount;
        energyUI.text = value + "% Energy Remaining";
    }

    void UpdateCashUi(int amount)
    {
        cashUI.text = "$" + amount;
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
        if (cash >= cost)
        {
            cash -= cost;
            return true;
        }

        return false;
    }

    public void TriggerPenalty()
    {
        mCash -= 8;
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