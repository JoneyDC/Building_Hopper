using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [Header("Expressive")]
    public static bool fireStarter; 
    public static float ChargeDownSpeed = 3f;
    public static float ChargeUpSpeed = 1f;
    public static bool superCharge;
    public static bool explode ;
    [Header("Abstract")]
    public static bool magneticShield;
    public static float ShieldRadius = 0.5f;
    public static float PullStrength = 1f;
    public static bool ressurection;
    public static bool doubleOrbs;
    [Header("Precision")]
    public static bool timeSlow;
    public static float MaxSlowCharge = 10f;
    public static float ChargeCost = 3f;
    public static float TimeScale = 0.6f;
    public static bool freeze;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("MySettings"))
        {
            string jsonData = PlayerPrefs.GetString("MySettings");
            Save loadedData = JsonUtility.FromJson<Save>(jsonData);
            fireStarter = loadedData.fireStarter;
            ChargeDownSpeed = loadedData.ChargeDownSpeed;
            ChargeUpSpeed = loadedData.ChargeUpSpeed;
            superCharge = loadedData.superCharge;
            explode = loadedData.explode;
            magneticShield = loadedData.magneticShield;
            ShieldRadius = loadedData.ShieldRadius;
            PullStrength = loadedData.PullStrength;
            ressurection = loadedData.ressurection;
            doubleOrbs = loadedData.doubleOrbs;
            timeSlow = loadedData.timeSlow;
            MaxSlowCharge = loadedData.MaxSlowCharge;
            ChargeCost = loadedData.ChargeCost;
            TimeScale = loadedData.TimeScale;
            freeze = loadedData.freeze;
        }
    }
    public void FireStarter()
    {
        fireStarter = true;
    }
    public void ChargeAmount()
    {
        ChargeDownSpeed -= 0.4f;
    }
    public void Explode()
    {
        explode = true;
    }
    public void SuperCharge()
    {
        superCharge = true;
    }
    public void TimeToCharge()
    {
        ChargeUpSpeed += 0.1f;
    }
    public void DoubleOrbs()
    {
        doubleOrbs = true;
    }
    public void LargerRadius()
    {
        ShieldRadius += 0.2f;
    }
    public void MagneticShield()
    {
        magneticShield = true;
    }
    public void Ressurection()
    {
        ressurection = true;
    }
    public void StrongerPull()
    {
        PullStrength += 1.5f;
    }
    public void ChargeCostReduction()
    {
        ChargeCost -= 0.5f;
    }
    public void Freeze()
    {
        freeze = true;
    }
    public void MaxChargeIncrease()
    {
        MaxSlowCharge += 2f;
    }
    public void TimeFrost()
    {
        TimeScale = 0.3f;
    }
    public void TimeSlow()
    {
        timeSlow = true;
    }
    private void OnDisable()
    {
        Save saveData = new Save();
        saveData.fireStarter = fireStarter;
        saveData.ChargeDownSpeed = ChargeDownSpeed;
        saveData.ChargeUpSpeed = ChargeUpSpeed;
        saveData.superCharge = superCharge;
        saveData.explode = explode;
        saveData.magneticShield = magneticShield;
        saveData.ShieldRadius = ShieldRadius;
        saveData.PullStrength = PullStrength;
        saveData.ressurection = ressurection;
        saveData.doubleOrbs = doubleOrbs;
        saveData.timeSlow = timeSlow;
        saveData.MaxSlowCharge = MaxSlowCharge;
        saveData.ChargeCost = ChargeCost;
        saveData.TimeScale = TimeScale;
        saveData.freeze = freeze;
        string jsonData = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("MySettings", jsonData);
        PlayerPrefs.Save();
    }

}
public class Save
{
    public bool fireStarter;
    public float ChargeDownSpeed;
    public float ChargeUpSpeed;
    public bool superCharge;
    public bool explode;
    public bool magneticShield;
    public float ShieldRadius ;
    public float PullStrength;
    public bool ressurection;
    public bool doubleOrbs;
    public bool timeSlow;
    public float MaxSlowCharge;
    public float ChargeCost;
    public float TimeScale;
    public bool freeze;
}

