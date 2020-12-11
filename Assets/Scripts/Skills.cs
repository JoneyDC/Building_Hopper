using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public GameObject FireStarterUI, FireChargeBar;
    [HideInInspector] public float FireCharge;
    [HideInInspector] public bool OnFire;
    public GameObject MagneticShield;
    [HideInInspector] public bool res;
    public GameObject TimeSlowUI, TimeChargeBar;
    float TimeCharge;
    bool slowing, OutOfEnergy;
    public bool Freeze;
    public GameObject PauseMenu;
    float TouchTimer;
    private void Awake()
    {
       if(Effects.fireStarter)
        {
            FireStarterUI.SetActive(true);
        }
       if(Effects.magneticShield)
        {
            MagneticShield.SetActive(true);
            MagneticShield.transform.localScale = new Vector3(Effects.ShieldRadius, Effects.ShieldRadius, 1f);
        }
       if(Effects.ressurection)
        {
            res = true;
        }
       if(Effects.timeSlow)
        {
            TimeSlowUI.SetActive(true);
            TimeCharge = Effects.MaxSlowCharge;
        }
       if(Effects.freeze)
        {
            Freeze = true;
        }

    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            TouchTimer += Time.unscaledDeltaTime;
        }
        else
        {
            TouchTimer = 0f;
        }
        if (Effects.fireStarter)
        {
            if (!OnFire)    
            {
                //time it takes to charge
                FireCharge += Time.deltaTime * Effects.ChargeUpSpeed;
            }
            else
            {
                //how long charge lasts
                FireCharge -= Time.deltaTime * Effects.ChargeDownSpeed;
            }
            if(FireCharge <= 0)
            {
                OnFire = false;
            }
            if(FireCharge > 10)
            {
                OnFire = true;
            }
            FireChargeBar.transform.localScale = new Vector3(1, FireCharge/ 10, 1);
        }
        if(Effects.timeSlow)
        {
            if (!PauseMenu.GetComponent<Pause_System>().Paused)
            {
                if (Input.touchCount > 0 && !GetComponent<PlayerMove>().GameOverScreen.activeSelf && !OutOfEnergy && TouchTimer > 0.2f)
                {
                    slowing = true;
                    Time.timeScale = Effects.TimeScale;
                }
                else if (!GetComponent<PlayerMove>().GameOverScreen.activeSelf)
                {
                    slowing = false;
                    Time.timeScale = 1f;
                }
                if (slowing)
                {
                    TimeCharge -= Time.unscaledDeltaTime * Effects.ChargeCost;
                }
                else if (TimeCharge < Effects.MaxSlowCharge)
                {
                    TimeCharge += Time.unscaledDeltaTime;
                }
                if (TimeCharge <= 0 && !GetComponent<PlayerMove>().GameOverScreen.activeSelf)
                {
                    slowing = false;
                    Time.timeScale = 1f;
                    OutOfEnergy = true;
                }
                if (TimeCharge > 5f)
                {
                    OutOfEnergy = false;
                }
                TimeChargeBar.transform.localScale = new Vector3(1, TimeCharge / Effects.MaxSlowCharge, 1);
            }
        }
    }
}
