using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    float PlayerExperience;
    int PlayerLevel;
    public GameObject bar;
    public Text text;
    float[] ExpToLevel;
    public GameObject SkillsTab, ExpressiveTab, AbstractTab, PrecisionTab, Select, SkillsTree;
    public Effects effects;
    void Start()
    {
        effects = GetComponent<Effects>();
        ExpToLevel = Restart.ExpToLevel;
        PlayerExperience = PlayerPrefs.GetFloat("PlayerExperience");
        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        if (PlayerLevel > ExpToLevel.Length)
        {
            bar.transform.localScale = new Vector3(PlayerExperience / ExpToLevel[ExpToLevel.Length - 1], 1, 1);
        }
        else
        {
            bar.transform.localScale = new Vector3(PlayerExperience / ExpToLevel[PlayerLevel], 1, 1);
        }
        text.text = PlayerLevel.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Resetprefs()
    {
        PlayerPrefs.SetFloat("PlayerExperience", 0);
        PlayerPrefs.SetInt("PlayerLevel", 0);
        PlayerPrefs.SetInt("PointsInExpressive", 0);
        PlayerPrefs.SetInt("PointsInAbstract", 0);
        PlayerPrefs.SetInt("PointsInPrecision", 0);
        PlayerPrefs.SetInt("SkillPoints", 0);
        bar.transform.localScale = new Vector3(PlayerPrefs.GetFloat("PlayerExperience") / ExpToLevel[0], 1, 1);
        text.text = PlayerPrefs.GetInt("PlayerLevel").ToString();
        Save saveData = new Save();
        saveData.fireStarter = false;
        saveData.ChargeDownSpeed = 3;
        saveData.ChargeUpSpeed = 1;
        saveData.superCharge = false;
        saveData.explode = false;
        saveData.magneticShield = false;
        saveData.ShieldRadius = 0.5f;
        saveData.PullStrength = 1;
        saveData.ressurection = false;
        saveData.doubleOrbs = false;
        saveData.timeSlow = false;
        saveData.MaxSlowCharge = 10;
        saveData.ChargeCost = 3;
        saveData.TimeScale = 0.6f;
        saveData.freeze = false;
        string jsonData = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("MySettings", jsonData);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
        Debug.Log("You need to dissable the OnDisable in Effects First");
    }
   public void OpenSkills()
    {
        if(SkillsTab.activeSelf)
        {
            SkillsTab.SetActive(false);
        }
        else
        {
            SkillsTab.SetActive(true);
        }
    }
    public void OpenExpressive()
    {
        if (ExpressiveTab.activeSelf)
        {
            ExpressiveTab.SetActive(false);
            Select.SetActive(false);
            SkillsTree.GetComponent<SkillTree>().EDes.text = "";
        }
        else
        {
            ExpressiveTab.SetActive(true);
        }
    }
    public void OpenAbstract()
    {
        if (AbstractTab.activeSelf)
        {
            AbstractTab.SetActive(false);
            Select.SetActive(false);
            SkillsTree.GetComponent<SkillTree>().ADes.text = "";
        }
        else
        {
            AbstractTab.SetActive(true);
        }
    }
    public void OpenPrecision()
    {
        if (PrecisionTab.activeSelf)
        {
            PrecisionTab.SetActive(false);
            Select.SetActive(false);
            SkillsTree.GetComponent<SkillTree>().PDes.text = "";
        }
        else
        {
            PrecisionTab.SetActive(true);
        }
    }
    public void GetSkillPoint()
    {
        SkillsTree.GetComponent<SkillTree>().Skillpoints += 1;
    }
}
