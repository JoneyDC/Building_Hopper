using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_System : MonoBehaviour
{
    int Pauses, tapCount;
    public GameObject PauseIcons, PauseMenu, Player;
    bool doubletap;
    public bool Paused;
    Vector2 lastTapPosition;
    float distance;
    private void Awake()
    {
        Pauses = 1;
        tapCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Pauses < 1)
        {
            PauseIcons.transform.GetChild(0).gameObject.SetActive(false);
            PauseIcons.transform.GetChild(1).gameObject.SetActive(false);
            PauseIcons.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (Pauses == 1)
        {
            PauseIcons.transform.GetChild(0).gameObject.SetActive(true);
            PauseIcons.transform.GetChild(1).gameObject.SetActive(false);
            PauseIcons.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (Pauses == 2)
        {
            PauseIcons.transform.GetChild(0).gameObject.SetActive(true);
            PauseIcons.transform.GetChild(1).gameObject.SetActive(true);
            PauseIcons.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (Pauses == 3)
        {
            PauseIcons.transform.GetChild(0).gameObject.SetActive(true);
            PauseIcons.transform.GetChild(1).gameObject.SetActive(true);
            PauseIcons.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (doubletap && Pauses > 0)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Pauses -= 1;
            tapCount = 0;
            doubletap = false;
            Paused = true;
        }
    }
    void LateUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            tapCount += 1;
            if (lastTapPosition == Vector2.zero)
            {
                lastTapPosition = Input.GetTouch(0).position;
                Debug.Log(lastTapPosition);
            }
            else
            {
                distance = Vector2.Distance(Input.GetTouch(0).position, lastTapPosition);
                Debug.Log(distance);
            }
            StartCoroutine(Countdown());
        }

        if (tapCount == 2 && distance <15)
        {
            tapCount = 0;
            StopCoroutine(Countdown());

            doubletap = true;
        }

    }
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.3f);
        lastTapPosition = Vector3.zero;
        tapCount = 0;
    }
    public void ClosePauseMenu()
    {
        PauseMenu.SetActive(false);
        Paused = false;
        Time.timeScale = 1;
        Player.GetComponent<PlayerMove>().bubble = true;
        Player.GetComponent<PlayerMove>().wings = true;
        Player.GetComponent<PlayerMove>().bubbleTimer += 5f;
        Player.GetComponent<PlayerMove>().wingsTimer += 5f;
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
}
