using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    GameObject GameOverScreen;
    private void Start()
    {
        GameOverScreen = GameObject.FindGameObjectWithTag("GameOver").transform.GetChild(0).gameObject;
    }
    public void RestartLevel()
    {
        GameOverScreen.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void MainMenu()
    {
        GameOverScreen.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
