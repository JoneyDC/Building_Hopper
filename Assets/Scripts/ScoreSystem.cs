using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Text Score, Experience;
    public GameObject ExperienceBar;
    [HideInInspector] public float exp, expLevel, distance;

    void Update()
    {
        distance += Time.deltaTime * MoveDown.GameSpeed;
        Score.text = distance.ToString("F0") + "<size=30>M</size> ";
        Experience.text = expLevel.ToString("F0");
        SetBar();
    }
    void SetBar()
    {
        ExperienceBar.transform.localScale = new Vector3(exp/1000, 1, 1);
        if(exp>1000)
        {
            exp -= 1000;
            expLevel += 1;
        }
    }
}
