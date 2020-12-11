using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheakIfButtonDisabled : MonoBehaviour
{
    public Upgrade upgrade;
    public bool Expressive, Abstract, Precision;
    int Points;

    private void Awake()
    {
        if (upgrade.AmountOfUpgrades <=0)
        {
            GetComponent<Button>().interactable = false;
        }
    }
    private void Update()
    {
        if (Expressive)
        {
            Points = PlayerPrefs.GetInt("PointsInExpressive");
        }
        if (Abstract)
        {
            Points = PlayerPrefs.GetInt("PointsInAbstract");
        }
        if (Precision)
        {
            Points = PlayerPrefs.GetInt("PointsInPrecision");
        }
        if (Points < upgrade.RequiredPoints)
        {
            GetComponent<Button>().interactable = false;
        }
        else if(upgrade.AmountOfUpgrades > 0)
        {
            GetComponent<Button>().interactable = true;
        }
    }
}

