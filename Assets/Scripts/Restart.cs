using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    float experience, distance, TotalScore;
    public Text text;
    public static float[] ExpToLevel = new float[] {2000,2500,3000,3500,4000,5000,6000,7000,8000,9000,10000,12000,14000,16000,18000,20000,25000,30000,35000,40000,45000,50000}; //subject to change


    private void Awake()
    {
        experience = GetComponentInParent<ScoreSystem>().expLevel;
        distance = GetComponentInParent<ScoreSystem>().distance;
        TotalScore = distance * (1 + experience) / 10;
        text.text = 
              "DISTANCE: " + distance.ToString("F0")+"M" + "\n"
            + "EXPERIENCE: " + experience.ToString("F0") + "\n"
            + "TOTAL XP GAIN: " + TotalScore.ToString("F0");
        float FinalExp = PlayerPrefs.GetFloat("PlayerExperience") + TotalScore;
        PlayerPrefs.SetFloat("PlayerExperience", FinalExp);
    }
    private void Update()
    {
        int PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        if (PlayerLevel < ExpToLevel.Length)
        {
            if (PlayerPrefs.GetFloat("PlayerExperience") > ExpToLevel[PlayerLevel])
            {
                float newExp = PlayerPrefs.GetFloat("PlayerExperience") - ExpToLevel[PlayerLevel];
                PlayerPrefs.SetFloat("PlayerExperience", newExp);
                PlayerLevel += 1;
                PlayerPrefs.SetInt("PlayerLevel", PlayerLevel);
                int Skillpoints = PlayerPrefs.GetInt("SkillPoints");
                Skillpoints += 1;
                PlayerPrefs.SetInt("SkillPoints",Skillpoints);
            }
        }
        else
        {
            if (PlayerPrefs.GetFloat("PlayerExperience") > 50000f)
            {
                float newExp = PlayerPrefs.GetFloat("PlayerExperience") - 50000;
                PlayerPrefs.SetFloat("PlayerExperience", newExp);
                PlayerLevel += 1;
                PlayerPrefs.SetInt("PlayerLevel", PlayerLevel);
                int Skillpoints = PlayerPrefs.GetInt("SkillPoints");
                Skillpoints += 1;
                PlayerPrefs.SetInt("SkillPoints", Skillpoints);
            }
        }
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
