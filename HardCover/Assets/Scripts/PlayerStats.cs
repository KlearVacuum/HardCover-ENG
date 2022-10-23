using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI knowledgeUI, energyUI, cashUI;
    private float m_Knowledge;
    public float knowledge
    {
        get => m_Knowledge; set { m_Knowledge = Mathf.Clamp(value, 0, 100); UpdateKnowledgeUI(m_Knowledge); }
    }
    private float m_Energy;
    public float energy
    {
        get => m_Energy; set { m_Energy = Mathf.Clamp(value, 0, 100); UpdateEnergyUI(m_Energy); }
    }
    private int m_Cash;
    public int cash
    {
        get => m_Cash; set { m_Cash = Mathf.Max(value, 0); UpdateCashUI(m_Cash); }
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

    void UpdateKnowledgeUI(float amount)
    {
        double value = amount;
        knowledgeUI.text = value + "% Knowledge Progress";
    }
    void UpdateEnergyUI(float amount)
    {
        double value = amount;
        energyUI.text = value + "% Energy Remaining";
    }
    void UpdateCashUI(int amount)
    {
        cashUI.text = "$" + amount;
    }
}
