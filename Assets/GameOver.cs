using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    GameObject GameOverScreen;
    void Start()
    {
        GameOverScreen = GameObject.FindGameObjectWithTag("GameOver").transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnControl>().MovementSpeed = 0;
            GameOverScreen.SetActive(true);
        }
    }
}
