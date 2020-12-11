using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    Upgrade SelectedUpgrade;
    Button SelectedButton;
    public GameObject Select, Effects;
    [HideInInspector] public int Skillpoints;
    int PointsInExpressive, PointsInAbstract, PointsInPrecision;
    public Text EDes, ADes, PDes, EPointsAvailable, APointsAvailable, PPointsAvailable, EPointsSpent, APointsSpent, PPointsSpent;

    private void Awake()
    {
        Skillpoints = PlayerPrefs.GetInt("SkillPoints");
        PointsInExpressive = PlayerPrefs.GetInt("PointsInExpressive");
        PointsInAbstract = PlayerPrefs.GetInt("PointsInAbstract");
        PointsInPrecision = PlayerPrefs.GetInt("PointsInPrecision");
    }
    private void Update()
    {
        EPointsAvailable.text = "Points Remaining: " + Skillpoints.ToString();
        EPointsSpent.text = "Points In Expressive " + PointsInExpressive.ToString();
        APointsAvailable.text = "Points Remaining: " + Skillpoints.ToString();
        APointsSpent.text = "Points In Abstract " + PointsInAbstract.ToString();
        PPointsAvailable.text = "Points Remaining: " + Skillpoints.ToString();
        PPointsSpent.text = "Points In Precision " + PointsInPrecision.ToString();
    }

    public void InitializeSkill(Upgrade upgrade)
    {
        Select.SetActive(true);
        SelectedUpgrade = upgrade;
        if(SelectedUpgrade.Type == "Expressive")
        {
            EDes.text = "Cost:" + SelectedUpgrade.UpgradeCost.ToString() + "\n" + SelectedUpgrade.Description;
        }
        if (SelectedUpgrade.Type == "Abstract")
        {
            ADes.text = "Cost:" + SelectedUpgrade.UpgradeCost.ToString() + "\n" + SelectedUpgrade.Description;
        }
        if (SelectedUpgrade.Type == "Precision")
        {
            PDes.text = "Cost:" + SelectedUpgrade.UpgradeCost.ToString() + "\n" + SelectedUpgrade.Description;
        }
    }
    public void SelectSkill()
    {
        if (Skillpoints >= SelectedUpgrade.UpgradeCost && SelectedUpgrade.AmountOfUpgrades>0)
        {
            SelectedUpgrade.AmountOfUpgrades -= 1;
            Skillpoints -= SelectedUpgrade.UpgradeCost;
            if(SelectedUpgrade.Type == "Expressive")
            {
                PointsInExpressive += SelectedUpgrade.UpgradeCost;
            }
            if (SelectedUpgrade.Type == "Abstract")
            {
                PointsInAbstract += SelectedUpgrade.UpgradeCost;
            }
            if (SelectedUpgrade.Type == "Precision")
            {
                PointsInPrecision += SelectedUpgrade.UpgradeCost;
            }
            Effects.SendMessage(SelectedUpgrade.Effect);

            if (SelectedUpgrade.AmountOfUpgrades <= 0)
            {
                SelectedButton.interactable = false;
            }
        }
        InitializePrefs();
    }
    public void SelectButton(Button button)
    {
        SelectedButton = button;
    }
    void InitializePrefs()
    {
        PlayerPrefs.SetInt("SkillPoints", Skillpoints);
        PlayerPrefs.SetInt("PointsInExpressive", PointsInExpressive);
        PlayerPrefs.SetInt("PointsInAbstract",PointsInAbstract);
        PlayerPrefs.SetInt("PointsInPrecision", PointsInPrecision);
    }
}
