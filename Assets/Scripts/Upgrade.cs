using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    public int UpgradeCost;
    public string Type;
    public string Effect;
    public int RequiredPoints;
    public int AmountOfUpgrades;
    public string Description;

}
